namespace AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings
{
    public class FlatControllerRemovedMessageComposer : MessageComposer
    {
        public FlatControllerRemovedMessageComposer(int roomId, int userId) : base(511)
        {
            AppendVL64(roomId);
            AppendVL64(userId);
        }
    }
}