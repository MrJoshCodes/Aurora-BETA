using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Handshake;

namespace AuroraEmu.Network.Game.Packets.Events.Handshake
{
    public class InitCryptoMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new SessionParamsMessageComposer());
        }
    }
}
