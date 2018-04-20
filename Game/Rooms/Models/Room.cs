using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Navigator.Models;
using AuroraEmu.Game.Rooms.Components;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AuroraEmu.Game.Rooms.Models
{
    public class Room : IDisposable
    {
        private int _virtualId;
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PlayersIn { get; set; }
        public int PlayersMax { get; set; }
        public int CategoryId { get; set; }
        public int Floor { get; set; }
        public int Wallpaper { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string CCTs { get; }
        public string Icon { get; set; }
        public double Landscape { get; set; }
        public bool DiagEnabled { get; set; } = true;
        public bool ShowOwner { get; set; }
        public bool AllPlayerRights { get; set; }
        public bool IsFrontpageItem =>
            Engine.Locator.NavigatorController.FrontpageItems.ContainsKey(Id);
        public RoomState State { get; set; }
        public FrontpageItem FrontpageItem =>
            Engine.Locator.NavigatorController.FrontpageItems[Id];
        private ConcurrentDictionary<int, Item> _items;
        public ConcurrentDictionary<int, Item> Items =>
            _items ?? (_items = Engine.Locator.ItemController.GetItemsInRoom(Id));
        public ConcurrentDictionary<int, RoomActor> Actors { get; set; }
        private ProcessComponent ProcessComponent { get; set; }
        public RoomGrid Grid { get; }
        public RoomMap Map { get; set; }
        public List<int> UserRights { get; }
        public bool Active => ProcessComponent != null;
        
        public ConcurrentDictionary<int, Item> ItemUpdates { get; set; }

        public Room()
        {
            State = RoomState.open;
            PlayersMax = 25;
            ShowOwner = true;
            AllPlayerRights = false;
            Icon = "HHIPAI";
            Landscape = 0.0;
        }

        public Room(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            OwnerId = reader.IsDBNull(reader.GetOrdinal("owner_id")) ? 0 : reader.GetInt32("owner_id");
            Name = reader.GetString("name");
            Description = reader.GetString("description");
            State = (RoomState)Enum.Parse(typeof(RoomState), reader.GetString("state"));
            PlayersIn = reader.GetInt32("players_in");
            PlayersMax = reader.GetInt32("players_max");
            CategoryId = reader.GetInt32("category_id");
            Model = reader.GetString("model");
            CCTs = reader.GetString("ccts");
            ShowOwner = reader.GetBoolean("show_owner");
            AllPlayerRights = reader.GetBoolean("all_player_rights");
            Icon = reader.GetString("icon");
            Floor = reader.GetInt32("floor");
            Wallpaper = reader.GetInt32("wallpaper");
            Landscape = reader.GetDouble("landscape");
            UserRights = new List<int>(JsonConvert.DeserializeObject<List<int>>(reader.GetString("user_rights")));
            if (Engine.Locator.RoomController.RoomMaps.TryGetValue(Model, out RoomMap map))
            {
                Map = map;
            }

            Actors = new ConcurrentDictionary<int, RoomActor>();
            ItemUpdates = new ConcurrentDictionary<int, Item>();

            ProcessComponent = new ProcessComponent(this);
            ProcessComponent.SetupRoomLoop();
            Grid = new RoomGrid(this);
        }

        public string Owner =>
            Engine.Locator.PlayerController.GetPlayerNameById(OwnerId);

        public int GetStateNumber() =>
            (int)State;

        public List<Item> GetFloorItems() =>
            Items.Values.Where(x => x.Definition.SpriteType.ToLower().Equals("s")).ToList();

        public List<Item> GetItems() =>
           Items.Values.ToList();

         public List<Item> GetItems(string interactorType) =>
           Items.Values.Where(x => x.Definition.InteractorType.ToLower().Equals(interactorType.ToLower())).ToList();

        public List<Item> GetWallItems() =>
            Items.Values.Where(x => x.Definition.SpriteType.ToLower().Equals("i")).ToList();

        public void SendComposer(MessageComposer composer)
        {
            foreach (RoomActor actor in Actors.Values)
            {
                actor.Client.SendComposer(composer);
            }
        }

        public void QueueComposer(MessageComposer composer)
        {
            foreach (RoomActor actor in Actors.Values)
            {
                actor.Client.QueueComposer(composer);
            }
        }

        public void FlushComposer(MessageComposer composer)
        {
            foreach (RoomActor actor in Actors.Values)
            {
                actor.Client.Flush();
            }
        }

        public void AddUserActor(Client client)
        {
            int newVirtualId = _virtualId++;

            UserActor actor = new UserActor(client, newVirtualId);
            Actors.TryAdd(newVirtualId, actor);
            client.CurrentRoomId = this.Id;
            client.CurrentRoom = Engine.Locator.RoomController.GetRoom(client.CurrentRoomId);
            client.UserActor = actor;
            PlayersIn++;

            if (CategoryId > 0)
            {
                Engine.Locator.NavigatorController.Categories[CategoryId].PlayersInside++;
            }
        }

        public void Save(string[] columns, object[] values)
        {
            if (columns.Length < 1 || columns.Length != values.Length)
            {
                return;
            }

            string query = "UPDATE rooms SET ";
            MySqlParameter[] parameters = new MySqlParameter[columns.Length + 1];

            for (int i = 0; i < columns.Length && i < values.Length; i++)
            {
                if (i > 0)
                    query += ", ";

                query += $"{columns[i]} = @{columns[i]}";
                parameters[i] = new MySqlParameter($"@{columns[i]}", values[i]);
            }

            query += " WHERE id = @roomId";
            parameters[parameters.Length - 1] = new MySqlParameter("@roomId", Id);

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(query);
                dbConnection.AddParameters(parameters);

                dbConnection.Execute();
            }
        }

        public void RemoveActor(RoomActor actor, bool closeConnection)
        {
            Actors.TryRemove(actor.VirtualId, out RoomActor roomActor);
            SendComposer(new UserRemoveMessageComposer(actor));
            PlayersIn--;

            // Since when switching rooms it SHOULDN'T close connection, this check:
            if (closeConnection)
                actor.Client.SendComposer(new CloseConnectionMessageComposer());
            actor.Dispose();
        }

        public void Loop()
        {
        }

        public void Dispose()
        {
            
            Engine.Locator.RoomController.Rooms.TryRemove(Id, out Room room);
            Actors.Clear();
            _items.Clear();
            Items.Clear();
            ProcessComponent.Dispose();
            ProcessComponent = null;
            Grid.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}