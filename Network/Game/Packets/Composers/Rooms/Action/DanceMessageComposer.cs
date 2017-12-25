namespace AuroraEmu.Network.Game.Packets.Composers.Rooms.Action
{
    public class DanceMessageComposer
    {
        public static MessageComposer Compose(int virtualId, int danceId)
        {
            var composer = new MessageComposer(480);
            composer.AppendVL64(virtualId);
            composer.AppendVL64(danceId);
            return composer;
        }
    }
}