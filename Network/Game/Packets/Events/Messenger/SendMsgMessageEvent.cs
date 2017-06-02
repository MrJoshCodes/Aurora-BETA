using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class SendMsgMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
        }
    }
}
