using AuroraEmu.Game.Badges;
using System.Collections.Generic;

namespace AuroraEmu.DI.Database.DAO
{
    public interface IBadgesDao
    {
        Dictionary<int, Badge> GetBadges(int playerId);
        void ClearBadgeSlots(int playerId);
        void UpdateBadgeSlots(int playerId, List<(int, int)> badges);
    }
}
