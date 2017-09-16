using System.Collections.Generic;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;

namespace AuroraEmu.DI.Database.DAO
{
    public interface INavigatorDao
    {
        Dictionary<int, FrontpageItem> ReloadFrontpageItems(Dictionary<int, FrontpageItem> frontpageItems);

        Dictionary<int, RoomCategory> ReloadCategories(Dictionary<int, RoomCategory> categories);

        List<Room> GetRoomsByOwner(int ownerId);

        List<Room> SearchRooms(string search);
        
        List<Room> GetRoomsByFriends(int playerId);
    }
}