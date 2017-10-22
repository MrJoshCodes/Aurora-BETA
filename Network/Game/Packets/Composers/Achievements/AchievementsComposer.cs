using System.Collections.Generic;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Achievements
{
    public class AchievementsComposer : MessageComposer
    {
        public AchievementsComposer(Dictionary<string, Achievement> achievements)
            : base(436)
        {
            AppendVL64(achievements.Count);

            foreach (var achievement in achievements.Values)
            {
                AppendVL64(achievement.Id);
                AppendVL64(achievement.Levels[1].Level);
                AppendString(achievement.Badge + achievement.Levels[1].Level);
            }
        }
    }
}