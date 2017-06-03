namespace AuroraEmu.Network.Game.Packets.Composers.Navigator
{
    class FlatCreatedComposer : MessageComposer
    {
        public FlatCreatedComposer(int roomId, string roomName) 
            :base(59)
        {
            AppendVL64(roomId);
            AppendString(roomName);
        }
    }
}
