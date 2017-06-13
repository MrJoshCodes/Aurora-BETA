using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Wordfilter;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class SendMsgMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int userId = msg.ReadVL64();
            string message = WordfilterController.GetInstance().CheckString(msg.ReadString());
            Client targetClient = ClientManager.GetInstance().GetClientByHabbo(userId);
            targetClient.SendComposer(new NewConsoleMessageComposer(message, client.Player.Id));
        }
    }
}
