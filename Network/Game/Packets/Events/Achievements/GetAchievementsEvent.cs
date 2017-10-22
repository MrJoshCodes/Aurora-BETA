using System.Collections.Generic;
using System.Linq;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Achievements;

namespace AuroraEmu.Network.Game.Packets.Events.Achievements
{
    public class GetAchievementsEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var achievements = Engine.Locator.AchievementController.Achievements;
            var userAchievements = client.Achievements;

            foreach (var kvp in userAchievements)
            {
                var achievement = achievements.Values.FirstOrDefault(x => x.Id == kvp.Key);

                if (achievement != null && kvp.Value >= achievement.Levels.Keys.Max())
                {
                    achievements.Remove(achievement.Badge);
                }
            }
            
            
            client.SendComposer(new AchievementsComposer(achievements, userAchievements));
        }
    }
}