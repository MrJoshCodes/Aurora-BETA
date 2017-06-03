using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class GetUserFlatCatsMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            MessageComposer tmp = new MessageComposer(221);
            tmp.AppendVL64(1);

            tmp.AppendVL64(0);
            tmp.AppendString("No Category");

            client.SendComposer(tmp);
        }
    }
}
