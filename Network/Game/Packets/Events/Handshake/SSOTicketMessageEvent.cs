using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Handshake
{
    public class SSOTicketMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.Login(msgEvent.ReadString());
        }
    }
}
