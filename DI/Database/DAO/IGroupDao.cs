using AuroraEmu.Game.Groups.Models;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IGroupDao
    {
        Group GetGroup(int id);
    }
}