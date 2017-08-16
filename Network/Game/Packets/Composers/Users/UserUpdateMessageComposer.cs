using AuroraEmu.Game.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Network.Game.Packets.Composers.Users
{
    public class UserUpdateMessageComposer : MessageComposer
    {
        public UserUpdateMessageComposer(List<RoomActor> actors)
            : base(34)
        {
            AppendVL64(actors.Count);
            foreach (RoomActor actor in actors)
            {
                AppendVL64(actor.VirtualId);
                AppendVL64(actor.Position.X);
                AppendVL64(actor.Position.Y);
                AppendString(actor.Position.Z.ToString("0.00"));
                AppendVL64(actor.Rotation);
                AppendVL64(actor.Rotation);
                
                AppendString("/", 0);

                foreach (KeyValuePair<string, string> Status in actor.Statusses.ToList())
                {
                    AppendString(Status.Key, 0);

                    if (!string.IsNullOrEmpty(Status.Value))
                    {
                        AppendString(" ", 0);
                        AppendString(Status.Value, 0);
                    }

                    AppendString("/", 0);
                }

                AppendString("/");
            }
        }
    }
}
