using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;

namespace AuroraEmu.Database.DAO
{
    public class NavigatorDao : INavigatorDao
    {
        public List<FrontpageItem> ReloadFrontpageItems(List<FrontpageItem> frontpageItems)
        {
            frontpageItems.Clear();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM frontpage_items;");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        frontpageItems.Add(new FrontpageItem(reader));

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            
            return frontpageItems;
        }

        public Dictionary<int, RoomCategory> ReloadCategories(Dictionary<int, RoomCategory> categories)
        {
            categories.Clear();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM room_categories");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        categories.Add(reader.GetInt32("id"), new RoomCategory(reader));

                    dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            
            return categories;
        }
        
        public List<Room> GetRoomsByOwner(int ownerId)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", ownerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = new Room(reader);
                        rooms.Add(room);
                        Engine.MainDI.RoomController.Rooms.AddOrUpdate(room.Id, room, (oldKey, oldValue) => room);
                    }

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return rooms;
        }

        public List<Room> SearchRooms(string search)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM rooms WHERE name LIKE @search OR owner_id IN (SELECT id FROM players WHERE username LIKE @search)");
                dbConnection.AddParameter("@search", "%" + search + "%");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = new Room(reader);
                        rooms.Add(room);
                        Engine.MainDI.RoomController.Rooms.AddOrUpdate(room.Id, room, (oldKey, oldValue) => room);
                    }

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return rooms;
        }
    }
}