namespace AuroraEmu.Network.Game.Packets.Composers.Handshake
{
    public class SessionParamsMessageComposer : MessageComposer
    {
        public SessionParamsMessageComposer() : base(257)
        {
            AppendString("RAHIIIKHJIPAIQAdd-MM-yyyy");
        }
    }
}
