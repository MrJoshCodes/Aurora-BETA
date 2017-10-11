using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Pathfinder;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Models
{
    public abstract class RoomActor : IDisposable
    {
        public int VirtualId { get; }
        public int Rotation { get; set; }
        public int StepsOnPath { get; set; }
        public bool QuitRoom { get; set; }
        public bool SetStep { get; set; }
        public bool UpdateNeeded { get; set; }
        public bool IsWalking =>
            (Path.Count > 0);
        public bool CalcPath { get; set; }
        public Client Client { get; }
        public Dictionary<string, string> Statusses { get; set; }
        public Point2D TargetPoint { get; set; }
        public Point2D Position { get; set; }
        public Point2D NextTile { get; set; }
        public IList<Point2D> Path { get; set; }
        public virtual ActorType Type { get; private set; }

        public RoomActor(Client client, int virtualId)
        {
            if (client.LoadingRoomId < 1)
                return;

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