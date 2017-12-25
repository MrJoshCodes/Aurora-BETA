using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms.Action;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms.Avatar
{
    public class DanceMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            var danceId = msgEvent.ReadVL64();
            
            // TODO: Check for HC
            
            client.CurrentRoom.SendComposer(DanceMessageComposer.Compose(client.UserActor.UserVirtualId, danceId));
        }
    }
}