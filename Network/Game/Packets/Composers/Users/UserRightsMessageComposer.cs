namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class UserRightsMessageComposer : MessageComposer
    {
        public UserRightsMessageComposer()
            : base(2)
        {
            base.AppendString("fuse_use_special_room_layouts");
        }
    }
}