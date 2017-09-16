using AuroraEmu.Game.Rooms;
using System.Collections.Generic;
using AuroraEmu.DI.Game.Navigator;

namespace AuroraEmu.Game.Navigator
{
    public class NavigatorController : INavigatorController
    {
        public Dictionary<int, FrontpageItem> FrontpageItems { get; set; }
        public Dictionary<int, RoomCategory> Categories { get; set; }


        public NavigatorController()
        {
            FrontpageItems = new Dictionary<int, FrontpageItem>();
            Categories = new Dictionary<int, RoomCategory>();

            ReloadFrontpageItems();
            ReloadCategories();
        }

        public void ReloadFrontpageItems()
        {
            Engine.MainDI.NavigatorDao.ReloadFrontpageItems(FrontpageItems);
            Engine.Logger.Info($"Loaded {FrontpageItems.Count} navigator frontpage items.");
        }

        public void ReloadCategories()
        {
            Engine.MainDI.NavigatorDao.ReloadCategories(Categories);
            Engine.Logger.Info($"Loaded {Categories.Count} room categories.");
        }

        public List<Room> GetRoomsByOwner(int ownerId)
        {
            return Engine.MainDI.NavigatorDao.GetRoomsByOwner(ownerId);
        }

        public List<Room> SearchRooms(string search)
        {
            return Engine.MainDI.NavigatorDao.SearchRooms(search);
        }

        public List<Room> GetRoomsByFriends(int playerId)
        {
            return Engine.MainDI.NavigatorDao.GetRoomsByFriends(playerId);
        }

        public List<RoomCategory> GetUserCategories(byte rank)
        {
            List<RoomCategory> categories = new List<RoomCategory>();

            foreach (RoomCategory category in Categories.Values)
            {
                if (category.MinRank <= rank)
                    categories.Add(category);
            }

            return categories;
        }
    }
}