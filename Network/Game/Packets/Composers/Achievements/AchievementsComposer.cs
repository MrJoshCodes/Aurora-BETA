using System.Collections.Generic;
using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Achievements
{
    public class AchievementsComposer : MessageComposer
    {
        public AchievementsComposer(Dictionary<string, Achievement> achievements, Dictionary<int, int> userAchievements)
            : base(436)
        {
            AppendVL64(achievements.Count);

            foreach (var achievement in achievements.Values)
            {
                int nextLevel;

                if (userAchievements.TryGetValue(achievement.Id, out int currentLevel))
                {
                    nextLevel = currentLevel + 1;
                }
                else
                {
                    nextLevel = 1;
                }
                
                AppendVL64(achievement.Id);
                AppendVL64(achievement.Levels[nextLevel].Level);
                AppendString(achievement.Badge + nextLevel);
            }
        }
    }
}