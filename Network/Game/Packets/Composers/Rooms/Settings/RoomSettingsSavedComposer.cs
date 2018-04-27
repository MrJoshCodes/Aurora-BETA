namespace AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings
{
    public class RoomSettingsSavedComposer : MessageComposer
    {
        public RoomSettingsSavedComposer(int roomId) : base(467)
        {
            AppendVL64(roomId);
        }
    }
}