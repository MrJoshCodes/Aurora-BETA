using AuroraEmu.Game.Navigator;
using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Navigator
{
    public interface INavigatorController
    {
        Dictionary<int, FrontpageItem> FrontpageItems { get; set; }

        Dictionary<int, RoomCategory> Categories { get; set; }

        void ReloadFrontpageItems();

        void ReloadCategories();

        List<Room> GetRoomsByOwner(int ownerId);

        List<Room> SearchRooms(string search);

        List<Room> GetRoomsByFriends(int playerId);

        List<RoomCategory> GetUserCategories(byte rank);
    }
}
