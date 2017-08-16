using AuroraEmu.Game.Rooms;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AuroraEmu.DI.Game.Rooms
{
    public interface IRoomController
    {
        ConcurrentDictionary<int, Room> Rooms { get; set; }

        Dictionary<string, RoomMap> RoomMaps { get; set; }

        void LoadRoomMaps();

        Room GetRoom(int id);

        int GetUserRoomCount(int userId);

        bool TryCreateRoom(string name, string model, int ownerId, out Room room);
    }
}
