using AuroraEmu.Game.Clients;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public class RoomActor
    {
        public Client Client { get; private set; }
        public int VirtualID { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Z { get; set; }
        public int Rotation { get; set; }
        public Dictionary<string, string> Statusses { get; set; }

        public RoomActor(Client client, int virtualId)
        {
            Client = client;
            VirtualID = virtualId;
            X = client.LoadingRoom.Map.DoorX;
            Y = client.LoadingRoom.Map.DoorY;
            Z = client.LoadingRoom.Map.DoorZ;
            Rotation = client.LoadingRoom.Map.DoorRotation;
            Statusses = new Dictionary<string, string>();
        }
    }
}
