using AuroraEmu.Game.Rooms;
using System.Collections.Generic;

namespace AuroraEmu.Network.Game.Packets.Composers.Rooms
{
    class UsersMessageComposer : MessageComposer
    {
        public UsersMessageComposer(RoomActor actor)
            : base(28)
        {
            AppendVL64(1);
            SerializeActor(actor);
        }

        public UsersMessageComposer(ICollection<RoomActor> actors)
            : base(28)
        {
            AppendVL64(actors.Count);

            foreach(RoomActor actor in actors)
            {
                SerializeActor(actor);
            }
        }

        private void SerializeActor(RoomActor actor)
        {
            AppendVL64(actor.Client.Player.Id);
            AppendString(actor.Client.Player.Username);
            AppendString(actor.Client.Player.Motto);
            AppendString(actor.Client.Player.Figure);
            AppendVL64(actor.VirtualID);
            AppendVL64(actor.X);
            AppendVL64(actor.Y);
            AppendString(actor.Z.ToString());
            AppendVL64(actor.Rotation);
            AppendVL64(1); // TODO: BOTs, PETs etc.
            AppendString(actor.Client.Player.Gender);
            AppendVL64(5);
            AppendVL64(-1);
            AppendVL64(-1);
            AppendString(""); // pool figure
        }
    }
}
