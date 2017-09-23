using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IRoomDao
    {
        void LoadRoomMaps(Dictionary<string, RoomMap> roomMaps);

        Room GetRoom(int id);

        int GetUserRoomCount(int userId);

        int GetRoomId(string name, string model, int ownerId);
    }
}