using System.Collections.Generic;
using System.Linq;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Navigator.Models;
using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Database.DAO
{
    public class NavigatorDao : INavigatorDao
    {
        public void ReloadFrontpageItems(Dictionary<int, FrontpageItem> frontpageItems)
        {
            frontpageItems.Clear();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM frontpage_items;");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        frontpageItems.Add(reader.GetInt32("room_id"), new FrontpageItem(reader));
            }
        }

        public void ReloadCategories(Dictionary<int, RoomCategory> categories)
        {
            categories.Clear();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM room_categories");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        categories.Add(reader.GetInt32("id"), new RoomCategory(reader));
            }
        }
        
        public List<Room> GetRoomsByOwner(int ownerId)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT `id` FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", ownerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = Engine.Locator.RoomController.GetRoom(reader.GetInt32("id"));
                        rooms.Add(room);
                    }
            }

            return rooms;
        }

        public List<Room> SearchRooms(string search)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT `id` FROM rooms WHERE name LIKE @search OR owner_id IN (SELECT id FROM players WHERE username LIKE @search)");
                dbConnection.AddParameter("@search", "%" + search + "%");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = Engine.Locator.RoomController.GetRoom(reader.GetInt32("id"));
                        rooms.Add(room);
                    }
            }

            return rooms;
        }

        public List<Room> GetRoomsByFriends(int playerId)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT `id` FROM rooms WHERE owner_id IN (SELECT `user_two_id` FROM `messenger_friends` WHERE `user_one_id` = @playerId) ORDER BY `players_in` DESC LIMIT 40");
                dbConnection.AddParameter("@playerId", playerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = Engine.Locator.RoomController.GetRoom(reader.GetInt32("id"));
                        rooms.Add(room);
                    }
            }

            return rooms;
        }

        public List<Room> GetTopRooms()
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT `id` FROM rooms ORDER BY `name` ASC LIMIT 40");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = Engine.Locator.RoomController.GetRoom(reader.GetInt32("id"));
                        rooms.Add(room);
                    }
            }

            return rooms.OrderByDescending(room => room.Actors.Count).ToList();
        }

        public List<Room> GetRoomsInCategory(int categoryId)
        {
            List<Room> rooms = new List<Room>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT `id` FROM rooms WHERE `category_id` = @categoryid ORDER BY `name` ASC LIMIT 40");
                dbConnection.AddParameter("@categoryid", categoryId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                    {
                        Room room = Engine.Locator.RoomController.GetRoom(reader.GetInt32("id"));
                        rooms.Add(room);
                    }
            }

            return rooms.OrderByDescending(room => room.Actors.Count).ToList();
        }
    }
}