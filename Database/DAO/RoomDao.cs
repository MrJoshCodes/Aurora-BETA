using AuroraEmu.DI.Database.DAO;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms;

namespace AuroraEmu.Database.DAO
{
    public class RoomDao : IRoomDao
    {
        public Dictionary<string, RoomMap> LoadRoomMaps(Dictionary<string, RoomMap> roomMaps)
        {
            roomMaps.Clear();
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM room_maps");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        roomMaps.Add(reader.GetString("name"), new RoomMap(reader));

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return roomMaps;
        }

        public Room GetRoom(int id)
        {
            Room room = null;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection(true))
            {
                dbConnection.Open();

                dbConnection.SetQuery("SELECT * FROM rooms WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                using(var reader = dbConnection.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        room = new Room(reader);
                        Engine.MainDI.RoomController.Rooms.TryAdd(id, room);
                    }
                }

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            if (room != null)
                return room;
            return null;
        }

        public int GetUserRoomCount(int userId)
        {
            int roomCount;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT COUNT(*) FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", userId);
                roomCount = int.Parse(dbConnection.GetString());

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            return roomCount;
        }

        public int GetRoomId(string name, string model, int ownerId)
        {
            int tmpRoomId;
            
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("INSERT INTO rooms (owner_id,name,model) VALUES (@ownerId, @name, @model)");
                dbConnection.AddParameter("@ownerId", ownerId);
                dbConnection.AddParameter("@name", name);
                dbConnection.AddParameter("@model", model);

                tmpRoomId = dbConnection.Insert();
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            
            return tmpRoomId;
        }
    }
}