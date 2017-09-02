using AuroraEmu.Game.Badges;
using System.Collections.Generic;
using System;
using System.Linq;
using AuroraEmu.Utilities;

namespace AuroraEmu.Game.Players.Components
{
    public class BadgesComponent
    {
        private int _playerId;

        public Dictionary<int, Badge> Badges { get; private set; }

        public BadgesComponent(int playerId)
        {
            _playerId = playerId;

            Badges = Engine.MainDI.BadgesDao.GetBadges(_playerId);
        }

        public bool TryGetBadge(string badge, out Badge b)
        {
            b = Badges.Values.Where(x => x.Code.Equals(badge)).FirstOrDefault();

            return b != null;
        }

        public void ClearBadgeSlots()
        {
            Badges.Values.ForEach(badge => badge.Slot = 0);
            Engine.MainDI.BadgesDao.ClearBadgeSlots(_playerId);
        }

        public void UpdateBadgeSlots(List<(int, int)> list)
        {
            Engine.MainDI.BadgesDao.UpdateBadgeSlots(_playerId, list);
        }

        public List<Badge> GetEquippedBadges()
        {
            List<Badge> badges = new List<Badge>();

            foreach (Badge badge in Badges.Values)
            {
                if (badge.Slot != 0)
                {
                    badges.Add(badge);
                }
            }

            return badges;
        }
    }
}
