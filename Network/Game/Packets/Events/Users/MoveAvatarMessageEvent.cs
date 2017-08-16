using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Game.Rooms.Pathfinder;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class MoveAvatarMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int x = msg.ReadVL64();
            int y = msg.ReadVL64();

            RoomActor actor = client.UserActor;
            actor.TargetPoint.X = x;
            actor.TargetPoint.Y = y;
            actor.CalcPath = true;
        }
    }
}
