using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class HabboSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string search = msg.ReadString();
            MessageComposer message = Engine.MainDI.MessengerController.MessengerSearch(search, client);
            client.SendComposer(message);
        }
    }
}
