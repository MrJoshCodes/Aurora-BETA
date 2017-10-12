using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.DI.Game.Badges
{
    public interface IBadgeController
    {
        IBadgesDao Dao { get; }
    }
}
