using System.Data;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IRoomDao
    {
        DataTable LoadRoomMaps();

        DataRow GetRoom(int id);

        int GetUserRoomCount(int userId);

        int GetRoomId(string name, string model, int ownerId);
    }
}