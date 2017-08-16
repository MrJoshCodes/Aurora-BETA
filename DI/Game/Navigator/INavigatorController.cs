using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Navigator
{
    public interface INavigatorController
    {
        List<FrontpageItem> FrontpageItems { get; set; }

        Dictionary<int, RoomCategory> Categories { get; set; }

        void ReloadFrontpageItems();

        void ReloadCategories();

        List<Room> GetRoomsByOwner(int ownerId);

        List<Room> SearchRooms(string search);

        List<RoomCategory> GetUserCategories(byte rank);
    }
}
