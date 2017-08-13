using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Game.Rooms.User;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public abstract class RoomActor
    {
        public Client Client { get; private set; }
        public int VirtualID { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Z { get; set; }
        public int Rotation { get; set; }
        public Dictionary<string, string> Statusses { get; set; }

        public bool SetStep { get; set; } = false;
        public int SetX { get; set; } = 0;
        public int SetY { get; set; } = 0;
        public bool UpdateNeeded { get; set; } = false;
        public bool IsWalking {
            get {
                return (Path.Count > 0);
            }
        }
        public bool CalcPath { get; set; } = false;
        public List<Point2D> Path { get; set; } = new List<Point2D>();
        public int TargetX { get; set; } = 0;
        public int TargetY { get; set; } = 0;
        public int StepsOnPath { get; set; } = 0;


        public Point2D PositionToSet { get; set; }
        public Point2D Position { get; set; }
        public ActorType Type { get; private set; }

        public RoomActor(Client client, int virtualId)
        {
            if (this is UserActor)
                Type = ActorType.User;
            Client = client;
            VirtualID = virtualId;
            X = client.LoadingRoom.Map.DoorX;
            Y = client.LoadingRoom.Map.DoorY;
            Z = client.LoadingRoom.Map.DoorZ;
            Rotation = client.LoadingRoom.Map.DoorRotation;
            Statusses = new Dictionary<string, string>();

            PositionToSet = new Point2D(X, Y);
            Position = new Point2D(X, Y);
        }
    }
}
