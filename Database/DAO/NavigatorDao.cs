using System.Collections.Generic;
using System.Data;
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

            DataTable result;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM frontpage_items;");

                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            foreach (DataRow row in result.Rows)
            {
                frontpageItems.Add(new FrontpageItem(row));
            }
            
            return frontpageItems;
        }

        public Dictionary<int, RoomCategory> ReloadCategories(Dictionary<int, RoomCategory> categories)
        {
            categories.Clear();
            DataTable result;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM room_categories");

                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            foreach(DataRow row in result.Rows)
            {
                categories.Add((int)row["id"], new RoomCategory(row));
            }
            
            return categories;
        }
        
        public List<Room> GetRoomsByOwner(int ownerId)
        {
            List<Room> rooms = new List<Room>();
            DataTable result;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", ownerId);
                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            foreach(DataRow row in result.Rows)
            {
                Room room = new Room(row);
                rooms.Add(room);
                Engine.MainDI.RoomController.Rooms.AddOrUpdate(room.Id, room, (oldKey, oldValue) => room);
            }

            return rooms;
        }
        
        public List<Room> SearchRooms(string search)
        {
            List<Room> rooms = new List<Room>();
            DataTable result;

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM rooms WHERE name LIKE @search OR owner_id IN (SELECT id FROM players WHERE username LIKE @search)");
                dbConnection.AddParameter("@search", "%" + search + "%");

                result = dbConnection.GetTable();
                
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            foreach (DataRow row in result.Rows)
            {
                Room room = new Room(row);
                rooms.Add(room);
                Engine.MainDI.RoomController.Rooms.AddOrUpdate(room.Id, room, (oldKey, oldValue) => room);
            }

            return rooms;
        }
    }
}