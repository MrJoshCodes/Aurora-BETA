using System;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

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

                Client targetClient = Engine.MainDI.ClientController.GetClientByHabbo(requestId);

                if (client.Player.MessengerComponent.IsFriends(requestId))
                    return;
                Engine.MainDI.MessengerDao.CreateFriendship(client.Player, requestId);
                Engine.MainDI.MessengerDao.DestroyRequest(client.Player.Id, requestId);
                Console.WriteLine(client.Player.Id + " " + requestId);
                client.Player.MessengerComponent.AddFriend(requestId, new MessengerFriend(requestId));
                client.SendComposer(new FriendListUpdateMessageComposer(client.Player.MessengerComponent.Friends));

                if (targetClient != null)
                    targetClient.SendComposer(new FriendListUpdateMessageComposer(targetClient.Player.MessengerComponent.Friends));
            }
        }
    }
}