using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Messenger;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class HabboSearchMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            string search = msg.ReadString();
            List<MessengerSearch> friends = new List<MessengerSearch>();
            List<MessengerSearch> notFriends = new List<MessengerSearch>(); 
            Engine.MainDI.MessengerController.MessengerSearch(search, client.Player, friends, notFriends);
            client.SendComposer(new HabboSearchResultMessageComposer(friends, notFriends));
        }
    }
}
