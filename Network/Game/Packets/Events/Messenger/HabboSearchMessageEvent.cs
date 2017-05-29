using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class HabboSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string search = msg.ReadString();
            MessageComposer message = MessengerController.GetInstance().MessengerSearch(search, client);
            client.SendComposer(message);
        }
    }
}
