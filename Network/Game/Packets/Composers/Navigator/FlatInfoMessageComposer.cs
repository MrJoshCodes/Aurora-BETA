using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class FlatInfoMessageComposer : MessageComposer
    {
        public FlatInfoMessageComposer(Room flat)
            : base(54)
        {
            AppendVL64(flat.AllPlayerRights);
            AppendVL64(flat.GetStateNumber());
            AppendVL64(flat.Id);
            AppendString(flat.Owner);
            AppendString(string.Empty);
            AppendString(flat.Name);
            AppendString(flat.Description);
            AppendVL64(flat.ShowOwner);
            AppendVL64(Engine.Locator.NavigatorController.Categories.TryGetValue(flat.CategoryId, out RoomCategory category) && category.TradeAllowed);
            AppendVL64(0);
            AppendVL64(flat.PlayersMax);
            AppendVL64(flat.PlayersMax);
        }
    }
}
