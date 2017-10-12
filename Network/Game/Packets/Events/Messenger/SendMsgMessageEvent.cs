using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Messenger;

namespace AuroraEmu.Network.Game.Packets.Events.Messenger
{
    public class SendMsgMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int userId = msg.ReadVL64();
            string message = Engine.Locator.WorldfilterController.CheckString(msg.ReadString());
            Client targetClient = Engine.Locator.ClientController.GetClientByHabbo(userId);
            targetClient.SendComposer(new NewConsoleMessageComposer(message, client.Player.Id));
        }
    }
}
