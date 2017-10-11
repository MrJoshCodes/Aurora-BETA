using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger.Models;
using AuroraEmu.Game.Players.Models;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class RequestBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string username = msg.ReadString();
            Player targetPlayer = Engine.MainDI.PlayerController.GetPlayerByName(username);

            Engine.MainDI.MessengerDao.CreateRequest(targetPlayer.Id, client);
            client.Player.MessengerComponent.AddRequest(targetPlayer.Id,
                new MessengerRequest(client.Player.Id, targetPlayer.Id));

            MessengerRequest messengerRequest = client.Player.MessengerComponent.GetRequest(targetPlayer.Id);

            Client targetClient = Engine.MainDI.ClientController.GetClientByHabbo(targetPlayer.Id);

            if (targetClient != null && messengerRequest != null)
                targetClient.SendComposer(
                    new NewBuddyRequestMessageComposer(messengerRequest, client.Player.Username));
        }
    }
}