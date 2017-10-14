namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class RoomAdMessageComposer : MessageComposer
    {
        public RoomAdMessageComposer()
            : base (208)
        {
            AppendString("http://127.0.0.1/r63/c_images/album1326/summer_habblet_US.png");
            AppendString("http://google.com");
        }
    }
}
