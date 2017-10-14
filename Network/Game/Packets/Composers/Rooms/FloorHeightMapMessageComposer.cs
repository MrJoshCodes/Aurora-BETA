namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class FloorHeightMapMessageComposer : MessageComposer
    {
        public FloorHeightMapMessageComposer(string heightmap)
            : base(470)
        {
            AppendString(heightmap);
        }
    }
}
