using System.Collections.Generic;
using System;
using System.Linq;
using AuroraEmu.Utilities;
using AuroraEmu.Game.Badges.Models;

namespace AuroraEmu.Game.Players.Components
{
    public class BadgesComponent : IDisposable
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

        public void UpdateBadgeSlots(List<(int, int)> list) =>
            Engine.MainDI.BadgesDao.UpdateBadgeSlots(_playerId, list);

        public List<Badge> GetEquippedBadges() =>
            Badges.Values.Where(badge => badge.Slot != 0).ToList();

        public void Dispose() =>
            Badges.Clear();
    }
}