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

            if (Engine.Locator.RoomController.TryCreateRoom(roomName, modelName, client.Player.Id, out int roomId))
            {
                client.SendComposer(new FlatCreatedComposer(roomId, roomName));
            }
            else
            {
                // Throw another error...
            }
        }
    }
}
