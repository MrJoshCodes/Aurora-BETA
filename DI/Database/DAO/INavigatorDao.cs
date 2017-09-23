using System.Collections.Generic;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;

namespace AuroraEmu.DI.Database.DAO
{
    public interface INavigatorDao
    {
        void ReloadFrontpageItems(Dictionary<int, FrontpageItem> frontpageItems);

        void ReloadCategories(Dictionary<int, RoomCategory> categories);

        List<Room> GetRoomsByOwner(int ownerId);

        List<Room> SearchRooms(string search);
        
        List<Room> GetRoomsByFriends(int playerId);
    }
}