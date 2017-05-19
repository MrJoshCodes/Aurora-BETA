using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Handshake
{
    public class InitCryptoMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            MessageComposer composer = new MessageComposer(257);
            composer.AppendString("RAHIIIKHJIPAIQAdd-MM-yyyy");
            client.SendComposer(composer);
        }
    }
}
