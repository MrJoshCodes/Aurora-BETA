namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class CanCreateRoomComposer : MessageComposer
    {
        public CanCreateRoomComposer(bool canCreate, int maxRooms)
            : base(512)
        {
            AppendVL64(canCreate);
            AppendVL64(maxRooms);
        }
    }
}
