using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Misc;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Action
{
    public class CallGuideBotMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new HabboBroadcastMessageComposer("Feature not implemented but enjoy your achievement..."));
            
            Engine.Locator.AchievementController.CheckAchievement(client, "ACH_Graduate", 1);
        }
    }
}