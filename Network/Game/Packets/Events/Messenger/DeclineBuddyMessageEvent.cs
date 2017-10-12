using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class DeclineBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int mode = msg.ReadVL64();
            int amount = msg.ReadVL64();

            if(mode == 0 && amount == 1)
            {
                int requestId = msg.ReadVL64();

                Engine.Locator.MessengerController.Dao.DestroyRequest(requestId, client.Player.Id);
            }
            else if(mode == 1)
            {
                Engine.Locator.MessengerController.Dao.DestroyAllRequests(client.Player.Id);
            }
        }
    }
}
