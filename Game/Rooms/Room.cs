using AuroraEmu.Database;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms.Components;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Game.Rooms.User;
using AuroraEmu.Network.Game.Packets;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;

namespace AuroraEmu.Game.Rooms
{
    public class Room : IDisposable
    {
        private int _virtualId;
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
        public string CCTs { get; }
        public bool ShowOwner { get; set; }
        public bool AllPlayerRights { get; set; }
        public bool IsFrontpageItem 
        { 
            get
            {
                return Engine.MainDI.NavigatorController.FrontpageItems.ContainsKey(Id);
            }
        }
        public FrontpageItem FrontpageItem 
        { 
            get
            {
                return Engine.MainDI.NavigatorController.FrontpageItems[Id];
            }
        }

        public string Icon { get; set; }
        public int Floor { get; set; }
        public int Wallpaper { get; set; }
        public double Landscape { get; set; }

        public RoomMap Map { get; set; }

        public bool DiagEnabled { get; set; } = true;

        public ConcurrentDictionary<int, Item> Items
        {
            get
            {
                if (_items == null)
                    _items = Engine.MainDI.ItemController.GetItemsInRoom(Id);

                return _items;
            }
        }
        public ConcurrentDictionary<int, RoomActor> Actors { get; private set; }
        private ProcessComponent ProcessComponent { get; set; }
        public bool[,] BlockedTiles { get; }

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

            if (Engine.MainDI.RoomController.RoomMaps.TryGetValue(Model, out RoomMap map))
            {
                Map = map;
            }

            BlockedTiles = new bool[map.MapSize.Item1, map.MapSize.Item2];

            Actors = new ConcurrentDictionary<int, RoomActor>();

            ProcessComponent = new ProcessComponent(this);
            ProcessComponent.SetupRoomLoop();

            foreach (Item item in Items.Values)
                if (item.Definition.ItemType == "trophy" ||
                    item.Definition.ItemType == "solid")
                    foreach (Point2D point in item.Tiles)
                        BlockedTiles[point.X, point.Y] = true;
        }

        public void AddUserActor(Client client)
        {
            int newVirtualId = _virtualId++;

            UserActor actor = new UserActor(client, newVirtualId);
            Actors.TryAdd(newVirtualId, actor);
            client.CurrentRoomId = this.Id;
            client.CurrentRoom = Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId);
            client.UserActor = actor;
            PlayersIn++;

            Engine.MainDI.NavigatorController.Categories[CategoryId].PlayersInside++;
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
            get { return Engine.MainDI.PlayerController.GetPlayerNameById(OwnerId); }
        }

        public int GetStateNumber()
        {
            return (int) State;
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

         public ConcurrentBag<Item> GetItems()
        {
            ConcurrentBag<Item> items = new ConcurrentBag<Item>();

            foreach (Item item in Items.Values)
            {
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

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(query);
                dbConnection.AddParameters(parameters);

                dbConnection.Execute();
            }
        }

        public void Loop()
        {
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

        public void Dispose()
        {
            Engine.MainDI.RoomController.Rooms.TryRemove(Id, out Room room);
            lock (Actors)
            {
                for (int i = 0; i < Actors.Count; i++)
                {
                    Actors[i].Dispose();
                }
            }
            Actors.Clear();
            _items.Clear();
            Items.Clear();
            ProcessComponent.Dispose();
            ProcessComponent = null;
        }
    }
}