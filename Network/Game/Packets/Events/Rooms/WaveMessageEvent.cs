using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class WaveMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoomId < 1)
                return;

            client.CurrentRoom.SendComposer(new WaveMessageComposer(client.UserActor.VirtualId));
        }
    }
}
