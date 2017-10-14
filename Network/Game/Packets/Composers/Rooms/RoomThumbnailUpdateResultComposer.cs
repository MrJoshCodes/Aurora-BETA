namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class RoomThumbnailUpdateResultComposer : MessageComposer
    {
        public RoomThumbnailUpdateResultComposer(int roomId)
            : base(457)
        {
            AppendVL64(roomId);
            AppendVL64(1); // ?
        }
    }
}
