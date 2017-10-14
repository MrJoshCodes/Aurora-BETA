using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class RemoveBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int requests = msg.ReadVL64();

            for (int i = 0; i < requests; i++)
            {
                int request = msg.ReadVL64();
                
                Engine.Locator.MessengerController.Dao.DestroyFriendship(request, client.Player.Id);
                client.Player.MessengerComponent.RemoveFriend(request);
                client.SendComposer(new FriendListUpdateMessageComposer(request));

                Client targetClient = Engine.Locator.ClientController.GetClientByHabbo(request);
                if (targetClient != null)
                {
                    targetClient.SendComposer(new FriendListUpdateMessageComposer(client.Player.Id));
                }
            }
        }
    }
}
