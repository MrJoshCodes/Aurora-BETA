using AuroraEmu.Game.Rooms.User;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms.Settings
{
    public class FlatControllerAddedComposer : MessageComposer
    {
        public FlatControllerAddedComposer(UserActor actor) : base(510)
        {
            AppendVL64(actor.Client.CurrentRoom.Id);
            AppendVL64(actor.Client.Player.Id);
            AppendString(actor.Client.Player.Username);
        }
    }
}
