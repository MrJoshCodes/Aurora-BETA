using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Players;
using AuroraEmu.Network.Game.Packets.Composers.Moderation;

namespace AuroraEmu.Network.Game.Packets.Events.Moderation
{
    public class GetModeratorUserInfoMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.Player.Rank <= 5) return;
            
            var playerId = msgEvent.ReadVL64();

            var player = Engine.MainDI.PlayerController.GetPlayerById(playerId);

            if (player == null) return;
            
            client.SendComposer(new ModeratorUserInfoComposer(player, Engine.MainDI.ClientController.GetClientByHabbo(playerId) != null));
        }
    }
}