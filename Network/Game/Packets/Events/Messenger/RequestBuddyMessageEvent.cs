using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class RequestBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string username = msg.ReadString();
            Player targetPlayer = PlayerController.GetInstance().GetPlayerByName(username);

            if (Engine.EnumToBool(targetPlayer.BlockNewFriends.ToString()))
            {
                client.SendComposer(new MessengerErrorMessageComposer());
                return;
            }

            MessengerController.GetInstance().CreateRequest(targetPlayer.Id, client);

            MessengerRequest request = MessengerController.GetInstance().GetRequest(client.Player.Id, targetPlayer.Id);
            Client targetClient = ClientManager.GetInstance().GetClientByHabbo(targetPlayer.Id);
            if (targetClient != null)
            {
                targetClient.SendComposer(new NewBuddyRequestMessageComposer(request, client.Player.Username));
            }
        }
    }
}
