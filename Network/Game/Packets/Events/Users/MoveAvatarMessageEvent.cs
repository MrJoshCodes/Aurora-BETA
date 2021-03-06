﻿using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Network.Game.Packets.Events.Users
{
    public class MoveAvatarMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msg)
        {
            int x = msg.ReadVL64();
            int y = msg.ReadVL64();

            if (client.CurrentRoomId == 0) return;
            if (!Engine.Locator.RoomController.Rooms.TryGetValue(client.CurrentRoomId, out Room room)) return;
            
            RoomActor actor = client.UserActor;

            if (x == room.Map.DoorX && y == room.Map.DoorY)
                actor.QuitRoom = true;
            if (actor.Position.X == x && actor.Position.Y == y) return;

            actor.TargetPoint.X = x;
            actor.TargetPoint.Y = y;
            actor.CalcPath = true;
        }
    }
}
