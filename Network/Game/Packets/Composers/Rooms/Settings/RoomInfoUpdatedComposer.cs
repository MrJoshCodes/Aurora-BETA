namespace AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings
{
    public class RoomInfoUpdatedComposer : MessageComposer
    {
        public RoomInfoUpdatedComposer(int roomId) : base(456)
        {
            AppendVL64(roomId);
        }
    }
}