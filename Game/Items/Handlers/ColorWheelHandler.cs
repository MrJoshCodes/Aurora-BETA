using System;
using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Items.Handlers
{
    public class ColorWheelHandler : IItemHandler
    {
        public void Handle(Item item, Client client)
        {
            item.Data = new Random().Next(1, 6).ToString();
        }

        public void Trigger(Item item, int request = 0)
        {
            item.Data = "-1";
        }
    }
}
