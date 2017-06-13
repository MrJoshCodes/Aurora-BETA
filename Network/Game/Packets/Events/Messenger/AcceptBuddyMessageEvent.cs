using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class AcceptBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int amount = msg.ReadVL64();
            System.Console.WriteLine(amount);
            for (int i = 0; i < amount; i++)
            {
                int requestId = msg.ReadVL64();

                Client targetClient = ClientManager.GetInstance().GetClientByHabbo(requestId);

                if (MessengerController.GetInstance().IsFriends(client, requestId))
                    return;
                MessengerController.GetInstance().CreateFriendship(requestId, client.Player.Id);

                client.SendComposer(MessengerController.GetInstance().UpdateFriendlist(client.Player.Id));

                if (targetClient != null)
                {
                    targetClient.SendComposer(MessengerController.GetInstance().UpdateFriendlist(targetClient.Player.Id));
                }
            }
        }
    }
}
