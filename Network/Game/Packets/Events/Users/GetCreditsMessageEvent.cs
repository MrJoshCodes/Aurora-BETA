using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class GetCreditsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            MessageComposer composer = new MessageComposer(6);
            composer.AppendString($"{client.Player.Coins}.0");
            client.SendComposer(composer);

            composer = new MessageComposer(438);
            composer.AppendVL64(client.Player.Pixels);
            composer.AppendVL64(0);
            client.SendComposer(composer);
        }
    }
}
