using System;
using System.Collections.Generic;
using System.Text;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Rooms;

namespace AuroraEmu.Network.Game.Packets.Events.Rooms
{
    class WaveMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            if (client.CurrentRoomId < 1)
                return;

            Room room = Engine.MainDI.RoomController.GetRoom(client.CurrentRoomId);

            room.SendComposer(new WaveMessageComposer(client.UserActor.VirtualId));
        }
    }
}
