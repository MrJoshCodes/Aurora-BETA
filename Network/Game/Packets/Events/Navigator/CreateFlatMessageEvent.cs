using AuroraEmu.Config;
using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms;
using AuroraEmu.Network.Game.Packets.Composers.Navigator;

namespace AuroraEmu.Network.Game.Packets.Events.Navigator
{
    class CreateFlatMessageEvent : IPacketEvent
    {
        public void Run(Client client, MessageEvent msgEvent)
        {
            //@Isome room@Gmodel_c@DopenA
            string roomName = msgEvent.ReadString();
            string modelName = msgEvent.ReadString();
            string state = msgEvent.ReadString(); // probs leftover from shockwave..
            
            //if (client.RoomCount >= ConfigLoader.GetInstance().HHConfig.MaxRooms)
            //{
            //    // Throw some error...
            //    return;
            //}

            if (RoomController.GetInstance().TryCreateRoom(roomName, modelName, client.Player.Id, out Room room))
            {
                client.SendComposer(new FlatCreatedComposer(room.Id, room.Name));
            }
            else
            {
                // Throw another error...
            }
        }
    }
}
