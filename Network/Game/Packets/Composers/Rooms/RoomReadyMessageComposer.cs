namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class RoomReadyMessageComposer : MessageComposer
    {
        public RoomReadyMessageComposer(int roomId, string model)
            : base(69)
        {
            AppendString(model);
            AppendVL64(roomId);
        }
    }
}
