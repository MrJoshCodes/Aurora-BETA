using AuroraEmu.Game.Badges;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IBadgesDao
    {
        Dictionary<int, Badge> GetBadges(int playerId);
    }
}
