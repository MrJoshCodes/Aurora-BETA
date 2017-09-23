using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Game.Rooms.User;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public abstract class RoomActor : IDisposable
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
        public bool QuitRoom { get; set; }

        public RoomActor(Client client, int virtualId)
        {
            if (client.LoadingRoomId < 1)
                return;

            if (this is UserActor)
                Type = ActorType.User;
            Room room = Engine.MainDI.RoomController.GetRoom(client.LoadingRoomId);
            Client = client;
            VirtualId = virtualId;
            Rotation = room.Map.DoorRotation;
            Statusses = new Dictionary<string, string>();

            Path = new List<Point2D>();
            TargetPoint = new Point2D(room.Map.DoorX, room.Map.DoorY, room.Map.DoorZ);
            Position = new Point2D(room.Map.DoorX, room.Map.DoorY, room.Map.DoorZ);
            NextTile = new Point2D(room.Map.DoorX, room.Map.DoorY, room.Map.DoorZ);
            SetStep = false;
            CalcPath = false;
            UpdateNeeded = true;
        }

        public void Dispose()
        {
            Statusses.Clear();
            Path.Clear();
            NextTile = null;
            Position = null;
            TargetPoint = null;
        }
    }
}
