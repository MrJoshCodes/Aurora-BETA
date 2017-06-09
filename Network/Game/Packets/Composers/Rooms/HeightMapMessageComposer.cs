namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class HeightMapMessageComposer : MessageComposer
    {
        public HeightMapMessageComposer(string map)
              : base(31)
        {
            foreach(string mapbit in map.Split('|'))
            {
                if (string.IsNullOrEmpty(mapbit))
                    continue;

                AppendString(mapbit, 13);
            }
        }
    }
}
