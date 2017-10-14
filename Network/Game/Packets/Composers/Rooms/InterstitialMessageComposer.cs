namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    public class InterstitialMessageComposer : MessageComposer
    {
        public InterstitialMessageComposer()
            : base(258)
        {
            // TODO: Ad some stuff here?
            AppendString("");
            AppendString("");
        }
    }
}