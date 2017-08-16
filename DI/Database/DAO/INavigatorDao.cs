using System.Collections.Generic;
using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;

namespace AuroraEmu.DI.Database.DAO
{
    public interface INavigatorDao
    {
        List<FrontpageItem> ReloadFrontpageItems(List<FrontpageItem> frontpageItems);

        Dictionary<int, RoomCategory> ReloadCategories(Dictionary<int, RoomCategory> categories);

        List<Room> GetRoomsByOwner(int ownerId);

        List<Room> SearchRooms(string search);
    }
}