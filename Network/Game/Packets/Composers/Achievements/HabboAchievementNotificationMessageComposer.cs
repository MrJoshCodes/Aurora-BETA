using AuroraEmu.Game.Achievements.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Achievements
{
    public class HabboAchievementNotificationMessageComposer : MessageComposer
    {
        public HabboAchievementNotificationMessageComposer(int level, Achievement achievement)
            : base(437)
        {
            AppendVL64(achievement.Id);
            AppendVL64(level);
            AppendString(achievement.Badge + level);
            AppendString(level > 1 ? achievement.Badge + (level - 1) : string.Empty);
        }
    }
}