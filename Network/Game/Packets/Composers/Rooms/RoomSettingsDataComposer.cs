using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class RoomSettingsDataComposer : MessageComposer
    {
        public RoomSettingsDataComposer(Room room)
            : base(465)
        {
            AppendVL64(room.Id);
            AppendString(room.Name);
            AppendString(room.Description);
            AppendVL64(room.GetStateNumber());
            AppendVL64(room.CategoryId);
            AppendVL64(room.PlayersMax);
            AppendVL64(25);
            AppendVL64(0); // no idea
            AppendVL64(0); // no idea
            AppendVL64(0); // no idea
            AppendVL64(0); // tags
            AppendVL64(0); // rights
            AppendVL64(0); // X users have rights
        }
    }
}
