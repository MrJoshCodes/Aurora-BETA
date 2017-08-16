using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Game.Rooms.User;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public abstract class RoomActor
    {
        public Client Client { get; }
        public int VirtualId { get; }
        public int Rotation { get; set; }
        public Dictionary<string, string> Statusses { get; set; }

        public bool SetStep { get; set; }
        public bool UpdateNeeded { get; set; }
        public bool IsWalking {
            get {
                return (Path.Count > 0);
            }
        }
        public bool CalcPath { get; set; }
        public IList<Point2D> Path { get; set; }
        public int StepsOnPath { get; set; }
        public Point2D TargetPoint { get; set; }
        public Point2D Position { get; set; }
        public Point2D NextTile { get; set; }

        public ActorType Type { get; private set; }

        public RoomActor(Client client, int virtualId)
        {
            if (this is UserActor)
                Type = ActorType.User;
            Client = client;
            VirtualId = virtualId;
            Rotation = client.LoadingRoom.Map.DoorRotation;
            Statusses = new Dictionary<string, string>();

            Path = new List<Point2D>();
            TargetPoint = new Point2D(client.LoadingRoom.Map.DoorX, client.LoadingRoom.Map.DoorY, client.LoadingRoom.Map.DoorZ);
            Position = new Point2D(client.LoadingRoom.Map.DoorX, client.LoadingRoom.Map.DoorY, client.LoadingRoom.Map.DoorZ);
            NextTile = new Point2D(client.LoadingRoom.Map.DoorX, client.LoadingRoom.Map.DoorY, client.LoadingRoom.Map.DoorZ);
            SetStep = false;
            CalcPath = false;
            UpdateNeeded = true;
        }
    }
}
