using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
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

                MessengerController.GetInstance().DestroyFriendship(request, client.Player.Id);
                client.SendComposer(new FriendListUpdateMessageComposer(request));

                Client targetClient = ClientManager.GetInstance().GetClientByHabbo(request);
                if (targetClient != null)
                {
                    targetClient.SendComposer(new FriendListUpdateMessageComposer(client.Player.Id));
                }
            }
        }
    }
}
