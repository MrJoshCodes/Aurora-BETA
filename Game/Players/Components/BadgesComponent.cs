using AuroraEmu.Game.Badges;
using System.Collections.Generic;

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
    }
}
