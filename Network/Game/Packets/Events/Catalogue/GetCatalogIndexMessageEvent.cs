using AuroraEmu.Game.Clients;

namespace AuroraEmu.Network.Game.Packets.Events.Catalogue
{
    class GetCatalogIndexMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            MessageComposer composer = new MessageComposer(126);
            composer.AppendVL64(false);
            composer.AppendVL64(0);
            composer.AppendVL64(0);
            composer.AppendVL64(-1);
            composer.AppendString("");
            composer.AppendVL64(false);

            Engine.Game.Catalog.SerializeIndex(composer);

            client.SendComposer(composer);
        }
    }
}
