using AuroraEmu.Game.Messenger.Models;
using AuroraEmu.Game.Players.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Messenger
{
    class BuddyRequestsMessageComposer : MessageComposer
    {
        public BuddyRequestsMessageComposer(Dictionary<int, MessengerRequest> requests)
            : base(314)
        {
            AppendVL64(requests.Count);
            AppendVL64(requests.Count);

            foreach (MessengerRequest request in requests.Values)
            {
                Player senderPlayer = Engine.MainDI.PlayerController.GetPlayerById(request.FromId);

                AppendVL64(request.FromId);
                AppendString(senderPlayer.Username);
                AppendString(request.FromId.ToString());
            }
        }
    }
}
