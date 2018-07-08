using AuroraEmu.DI.Database.DAO;
using System.Collections.Generic;
using AuroraEmu.Game.Rooms.Models;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Database.DAO
{
    public class RoomDao : IRoomDao
    {
        public void LoadRoomMaps(Dictionary<string, RoomMap> roomMaps)
        {
            roomMaps.Clear();
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM room_maps");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        roomMaps.Add(reader.GetString("name"), new RoomMap(reader));
            }
        }

        public Room GetRoom(int id)
        {
            Room room = null;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM rooms WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                using(var reader = dbConnection.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        room = new Room(reader);
                        Engine.Locator.RoomController.Rooms.TryAdd(id, room);
                    }
                }
            }
            return room ?? null;
        }

        public int GetUserRoomCount(int userId)
        {
            int roomCount;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT COUNT(*) FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", userId);
                roomCount = int.Parse(dbConnection.GetString());
            }
            return roomCount;
        }

        public int GetRoomId(string name, string model, int ownerId)
        {
            int tmpRoomId;
            
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO rooms (owner_id,name,model,description,ccts) VALUES (@ownerId, @name, @model, '', '')");
                dbConnection.AddParameter("@ownerId", ownerId);
                dbConnection.AddParameter("@name", name);
                dbConnection.AddParameter("@model", model);

                tmpRoomId = dbConnection.Insert();
            }
            
            return tmpRoomId;
        }

        public List<string> GetRoomTags(int id)
        {
            List<string> tags = new List<string>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT tag FROM room_tags WHERE room_id = @roomId");
                dbConnection.AddParameter("@roomId", id);

                using (MySqlDataReader reader = dbConnection.ExecuteReader())
                {
                    while (reader.Read())
                        tags.Add(reader.GetString("tag"));
                }
            }

            return tags;
        }
    }
}