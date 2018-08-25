using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Handshake;

namespace AuroraEmu.Network.Game.Packets.Events.Handshake
{
    class GenerateSecretKeyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            client.SendComposer(new SessionParamsMessageComposer());
        }
    }
}
