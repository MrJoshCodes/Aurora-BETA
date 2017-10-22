using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Achievements;

namespace AuroraEmu.Network.Game.Packets.Events.Achievements
{
    public class GetAchievementsEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new AchievementsComposer(Engine.Locator.AchievementController.Achievements));
        }
    }
}