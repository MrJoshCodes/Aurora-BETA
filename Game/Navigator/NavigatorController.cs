using AuroraEmu.Database;
using AuroraEmu.Game.Rooms;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Navigator
{
    public class NavigatorController
    {
        private static NavigatorController instance;

        public List<FrontpageItem> FrontpageItems { get; private set; }

        public NavigatorController()
        {
            FrontpageItems = new List<FrontpageItem>();

            ReloadFrontpageItems();
        }

        public void ReloadFrontpageItems()
        {
            FrontpageItems.Clear();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM frontpage_items;");
                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            foreach(DataRow row in result.Rows)
            {
                FrontpageItems.Add(new FrontpageItem(row));
            }

            Engine.Logger.Info($"Loaded {FrontpageItems.Count} navigator frontpage items.");
        }

        public List<Room> GetRoomsByOwner(int ownerId)
        {
            List<Room> rooms = new List<Room>();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", ownerId);
                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            foreach(DataRow row in result.Rows)
            {
                Room room = new Room(row);
                rooms.Add(room);
                RoomController.GetInstance().Rooms.AddOrUpdate(room.Id, room, (old_key, old_value) => room);
            }

            return rooms;
        }

        public static NavigatorController GetInstance()
        {
            if (instance == null)
                instance = new NavigatorController();

            return instance;
        }
    }
}