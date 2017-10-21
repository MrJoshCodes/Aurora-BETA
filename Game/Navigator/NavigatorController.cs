using System.Collections.Generic;
using AuroraEmu.DI.Game.Navigator;
using System.Linq;
using AuroraEmu.Game.Navigator.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Game.Navigator
{
    public class NavigatorController : INavigatorController
    {
        public Dictionary<int, FrontpageItem> FrontpageItems { get; set; }
        public Dictionary<int, RoomCategory> Categories { get; set; }
        public INavigatorDao Dao { get; }

        public NavigatorController(INavigatorDao dao)
        {
            Dao = dao;
            FrontpageItems = new Dictionary<int, FrontpageItem>();
            Categories = new Dictionary<int, RoomCategory>();

            ReloadFrontpageItems();
            ReloadCategories();
        }

        public void ReloadFrontpageItems()
        {
            Dao.ReloadFrontpageItems(FrontpageItems);
            Engine.Logger.Info($"Loaded {FrontpageItems.Count} navigator frontpage items.");
        }

        public void ReloadCategories()
        {
            Dao.ReloadCategories(Categories);
            Engine.Logger.Info($"Loaded {Categories.Count} room categories.");
        }

        public List<Room> GetRoomsByOwner(int ownerId) =>
            Dao.GetRoomsByOwner(ownerId);

        public List<Room> SearchRooms(string search) =>
            Dao.SearchRooms(search);

        public List<Room> GetRoomsByFriends(int playerId) =>
            Dao.GetRoomsByFriends(playerId);

        public List<RoomCategory> GetUserCategories(byte rank) =>
            Categories.Values.Where(catecory => catecory.MinRank <= rank).ToList();

        public List<Room> GetTopRooms() =>
            Dao.GetTopRooms();

        public List<Room> GetRoomsInCategory(int categoryId) =>
            Dao.GetRoomsInCategory(categoryId);
    }
}