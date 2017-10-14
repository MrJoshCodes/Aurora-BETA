using System;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using AuroraEmu.Game.Messenger.Models;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class AcceptBuddyMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int amount = msg.ReadVL64();
            for (int i = 0; i < amount; i++)
            {
                int requestId = msg.ReadVL64();

                Client targetClient = Engine.Locator.ClientController.GetClientByHabbo(requestId);

                if (client.Player.MessengerComponent.IsFriends(requestId))
                    return;
                Engine.Locator.MessengerController.Dao.CreateFriendship(client.Player, requestId);
                Engine.Locator.MessengerController.Dao.DestroyRequest(client.Player.Id, requestId);
                Console.WriteLine(client.Player.Id + " " + requestId);
                client.Player.MessengerComponent.AddFriend(requestId, new MessengerFriend(requestId));
                client.SendComposer(new FriendListUpdateMessageComposer(client.Player.MessengerComponent.Friends));

                if (targetClient != null)
                    targetClient.SendComposer(new FriendListUpdateMessageComposer(targetClient.Player.MessengerComponent.Friends));
            }
        }
    }
}