using AuroraEmu.Game.Rooms.Models;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class GuestRoomSearchResultComposer : MessageComposer
    {
        public GuestRoomSearchResultComposer(int tab, string search, List<Room> rooms)
            : base(451)
        {
            AppendVL64(tab);
            AppendString(search);
            AppendVL64(rooms.Count);

            foreach(Room room in rooms)
            {
                SerializeRoom(room, this);
            }
        }

        public static void SerializeRoom(Room room, MessageComposer composer)
        {
            composer.AppendVL64(room.Id);
            composer.AppendVL64(false); // events
            composer.AppendString(room.Name);
            composer.AppendString(room.Owner);
            composer.AppendVL64((int)room.State);
            composer.AppendVL64(room.PlayersIn);
            composer.AppendVL64(room.PlayersMax);
            composer.AppendString(room.Description);
            composer.AppendVL64(0);
            composer.AppendVL64(Engine.Locator.NavigatorController.Categories.ContainsKey(room.CategoryId) && Engine.Locator.NavigatorController.Categories[room.CategoryId].TradeAllowed);
            composer.AppendVL64(0); // score
            composer.AppendVL64(room.CategoryId);
            composer.AppendString("");
            composer.AppendVL64(room.Tags.Count);

            foreach (string tag in room.Tags)
            {
                composer.AppendString(tag);
            }

            composer.AppendString(room.Icon, 0);
        }
    }
}
