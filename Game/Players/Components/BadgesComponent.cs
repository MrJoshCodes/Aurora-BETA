using System.Collections.Generic;
using System;
using System.Globalization;
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

            Badges = Engine.Locator.BadgeController.Dao.GetBadges(_playerId);
        }

        public bool TryGetBadge(string badge, out Badge b)
        {
            b = Badges.Values.FirstOrDefault(x => x.Code.Equals(badge));

            return b != null;
        }

        public void ClearBadgeSlots()
        {
            Badges.Values.ForEach(badge => badge.Slot = 0);
            Engine.Locator.BadgeController.Dao.ClearBadgeSlots(_playerId);
        }

        public void UpdateBadgeSlots(List<(int, int)> list) =>
            Engine.Locator.BadgeController.Dao.UpdateBadgeSlots(_playerId, list);

        public List<Badge> GetEquippedBadges() =>
            Badges.Values.Where(badge => badge.Slot != 0).ToList();

        public void Dispose() =>
            Badges.Clear();

        public void AddOrUpdateBadge(string achievementBadge, int checkLevel)
        {
            var badge = Badges.Values.FirstOrDefault(x => x.Code.Equals(achievementBadge + (checkLevel - 1)));

            if (badge == null)
            {
                int id = Engine.Locator.BadgeController.Dao.InsertBadge(_playerId, achievementBadge + checkLevel);
                Badges.Add(id, new Badge(id, achievementBadge + checkLevel));
            }
            else
            {
                Engine.Locator.BadgeController.Dao.UpdateBadge(_playerId, achievementBadge + checkLevel);
                Badges[badge.Id] = new Badge(badge.Id, achievementBadge + checkLevel, badge.Slot);
            }
        }
    }
}