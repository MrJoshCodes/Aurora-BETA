namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class RoomEntryInfoMessageComposer : MessageComposer
    {
        public RoomEntryInfoMessageComposer(bool privateRoom, int roomId, bool rights)
            : base(471)
        {
            AppendVL64(privateRoom);
            AppendVL64(roomId);
            AppendVL64(rights);
        }
    }
}
