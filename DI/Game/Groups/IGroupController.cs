using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.DI.Game.Groups
{
    public interface IGroupController
    {
        IGroupDao Dao { get; }
    }
}