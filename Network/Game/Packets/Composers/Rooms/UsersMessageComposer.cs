using AuroraEmu.Game.Rooms.Models;
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
            AppendVL64(actor.VirtualId);
            AppendVL64(actor.Position.X);
            AppendVL64(actor.Position.Y);
            AppendString(actor.Position.Z.ToString());
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
