using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Players;
using AuroraEmu.Game.Rooms.Components;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Network.Game.Packets;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class Room
    {
        private int virtualId = 0;
        private ConcurrentDictionary<int, Item> _items;

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoomState State { get; set; }
        public int PlayersIn { get; set; }
        public int PlayersMax { get; set; }
        public int CategoryId { get; set; }
        public string Model { get; set; }
        public string CCTs { get; private set; }
        public bool ShowOwner { get; set; }
        public bool AllPlayerRights { get; set; }
        public string Icon { get; set; }
        public int Floor { get; set; }
        public int Wallpaper { get; set; }
        public double Landscape { get; set; }

        public RoomMap Map { get; private set; }

        public bool DiagEnabled { get; set; } = true;

        public ConcurrentDictionary<int, Item> Items
        {
            get
            {
                if (_items == null)
                    _items = ItemController.GetInstance().GetItemsInRoom(Id);

                return _items;
            }
        }

        public ConcurrentDictionary<int, RoomActor> Actors { get; private set; }
        private ProcessComponent ProcessComponent { get; set; }

        public Room()
        {
            State = RoomState.open;
            PlayersMax = 25;
            ShowOwner = true;
            AllPlayerRights = false;
            Icon = "HHIPAI";
            Landscape = 0.0;
        }
        public Room(DataRow row)
        {
            Id = (int)row["id"];
            OwnerId = row["owner_id"].GetType().Equals(typeof(DBNull)) ? 0 : (int)row["owner_id"];
            Name = (string)row["name"];
            Description = (string)row["description"];
            State = (RoomState)Enum.Parse(typeof(RoomState), (string)row["state"]);
            PlayersIn = (int)row["players_in"];
            PlayersMax = (int)row["players_max"];
            CategoryId = (int)row["category_id"];
            Model = (string)row["model"];
            CCTs = (string)row["ccts"];
            ShowOwner = (bool)row["show_owner"];
            AllPlayerRights = (bool)row["all_player_rights"];
            Icon = (string)row["icon"];
            Floor = (int)row["floor"];
            Wallpaper = (int)row["wallpaper"];
            Landscape = (double)row["landscape"];
            Map = RoomController.GetInstance().RoomMaps[Model];
            Actors = new ConcurrentDictionary<int, RoomActor>();

            ProcessComponent = new ProcessComponent(this);
            ProcessComponent.SetupRoomLoop();
        }

        public void AddUserActor(Client client)
        {
            int newVirtualId = virtualId++;
            
            UserActor actor = new UserActor(client, newVirtualId);
            Actors.TryAdd(newVirtualId, actor);

            client.CurrentRoom = this;
            client.LoadingRoom = null;
            client.RoomActor = actor;
            client.CurrentRoom.PlayersIn++;

            NavigatorController.GetInstance().Categories[client.CurrentRoom.CategoryId].PlayersInside++;
        }

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

        public string Owner
        {
            get
            {
                return PlayerController.GetInstance().GetPlayerNameById(OwnerId);
            }
        }
        
        public int GetStateNumber()
        {
            return (int)State;
        }

        public ConcurrentBag<Item> GetFloorItems()
        {
            ConcurrentBag<Item> items = new ConcurrentBag<Item>();

            foreach (Item item in Items.Values)
            {
                if (item.Definition.SpriteType.ToLower().Equals("s"))
                    items.Add(item);
            }

            return items;
        }

        public ConcurrentBag<Item> GetWallItems()
        {
            ConcurrentBag<Item> items = new ConcurrentBag<Item>();

            foreach (Item item in Items.Values)
            {
                if (item.Definition.SpriteType.ToLower().Equals("i"))
                    items.Add(item);
            }

            return items;
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

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery(query);
                dbConnection.AddParameters(parameters);
                dbConnection.Open();

                dbConnection.Execute();
            }
        }

        public void Loop()
        {

        }
    }
}
