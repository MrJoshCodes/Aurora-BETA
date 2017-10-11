using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Items.Models.Dimmer;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using System.Linq;

namespace AuroraEmu.Network.Game.Packets.Events.Items
{
    public class RoomDimmerChangeStateMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            Item item = client.CurrentRoom.GetWallItems().Where(x => x.Definition.HandleType == AuroraEmu.Game.Items.Handlers.HandleType.DIMMER).ToList()[0];
            DimmerData data = Engine.MainDI.ItemController.GetDimmerData(item.Id);

            if (data.Enabled)
                data.Enabled = false;
            else
                data.Enabled = true;
            item.Data = data.GenerateExtradata();
            client.CurrentRoom.SendComposer(new ItemUpdateMessageComposer(item));
            Engine.MainDI.ItemDao.UpdateItemData(item.Id, item.Data);
        }
    }
}
