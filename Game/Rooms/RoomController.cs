using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class RoomController
    {
        private static RoomController instance;

        public ConcurrentDictionary<int, Room> Rooms { get; private set; }
        public Dictionary<string, RoomMap> RoomMaps { get; private set; }

        public RoomController()
        {
            Rooms = new ConcurrentDictionary<int, Room>();
            RoomMaps = new Dictionary<string, RoomMap>();
        }

        public void LoadRoomMaps()
        {
            RoomMaps.Clear();

            DataTable table;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM room_maps");
                dbConnection.Open();

                table = dbConnection.GetTable();
            }

            foreach(DataRow row in table.Rows)
            {
                RoomMaps.Add((string)row["name"], new RoomMap(row));
            }

            Engine.Logger.Info($"Loaded {RoomMaps.Count} room maps.");
        }

        public Room GetRoom(int id)
        {
            if (Rooms.TryGetValue(id, out Room room))
                return room;

            DataRow row;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM rooms WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                dbConnection.Open();

                row = dbConnection.GetRow();
            }

            if (row != null)
            {
                room = new Room(row);
                Rooms.TryAdd(id, room);

                return room;
            }

            return null;
        }

        public int GetUserRoomCount(int userId)
        {
            int roomCount = 0;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT COUNT(*) FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", userId);
                dbConnection.Open();

                roomCount = int.Parse(dbConnection.GetString());
            }

            return roomCount;
        }

        public bool TryCreateRoom(string name, string model, int ownerId, out Room room)
        {
            Room tmp_room = new Room()
            {
                Name = name,
                Model = model,
                OwnerId = ownerId
            };

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("INSERT INTO rooms (owner_id,name,model) VALUES (@ownerId, @name, @model)");
                dbConnection.AddParameter("@ownerId", ownerId);
                dbConnection.AddParameter("@name", name);
                dbConnection.AddParameter("@model", model);
                dbConnection.Open();

                tmp_room.Id = dbConnection.Insert();
            }

            if (tmp_room.Id > 0)
            {
                Rooms.TryAdd(tmp_room.Id, tmp_room);
                room = tmp_room;
                return true;
            }

            room = null;
            return false;
        }

        public static RoomController GetInstance()
        {
            if (instance == null)
                instance = new RoomController();

            return instance;
        }
    }
}
