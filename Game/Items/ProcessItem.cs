using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Items
{
    public class ProcessItem
    {
        public void Process(Item item, Client interactor, int cycles = 0)
        {
            Task.Run(async delegate
            {
                item.Cycling = true;
                await Task.Delay(cycles * 500);
                if (Engine.Locator.ItemController.Handlers.TryGetValue(item.Definition.HandleType, out IItemHandler itemHandler))
                {
                    itemHandler.Handle(item, interactor);
                }
                if (item.Definition.SpriteType == "i")
                    interactor.CurrentRoom.SendComposer(new ItemUpdateMessageComposer(item));
                else
                    interactor.CurrentRoom.SendComposer(new ObjectDataUpdateMessageComposer(item.Id, item.Data));
                Engine.Locator.ItemController.Dao.UpdateItemData(item.Id, item.Data);
                item.Cycling = false;
            });
        }
    }
}
