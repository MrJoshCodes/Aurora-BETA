using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    public class QuitMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.CurrentRoom.RemoveActor(client.UserActor, true);
        }
    }
}
