using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game.Badges;

namespace AuroraEmu.Game.Badges
{
    public class BadgeController : IBadgeController
    {
        public IBadgesDao Dao { get; }

        public BadgeController(IBadgesDao dao)
        {
            Dao = dao;
        }
    }
}
