using AuroraEmu.Game.Items;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Network.Game.Packets.Composers.Users;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessComponent : IDisposable
    {
        private Room room;
        private CancellationTokenSource _wtoken;
        private ActionBlock<DateTimeOffset> _task;
        private int idleRoom = 0;

        public ProcessComponent(Room room)
        {
            this.room = room;
        }

        public void SetupRoomLoop()
        {
            _wtoken = new CancellationTokenSource();

            _task = Engine.MainDI.TaskController.ExecutePeriodic(now => Loop(), _wtoken.Token, 500);
            _task.Post(DateTimeOffset.Now);
        }

        private void Loop()
        {
            room.Loop();
            if (idleRoom > 120)
                room.Dispose();

            if (!(room.Actors.Count > 0))
                idleRoom++;
            else
                idleRoom = 0;

            List<RoomActor> toUpdate = new List<RoomActor>();
            foreach (RoomActor actor in room.Actors.Values)
            {
                #region walking related

                if (actor.SetStep)
                {
                    room.Grid.EntityGrid[actor.Position.X, actor.Position.Y] = false;
                    room.Grid.EntityGrid[actor.NextTile.X, actor.NextTile.Y] = true;
                    actor.Position = actor.NextTile;
                    actor.Position.Z = Math.Round(room.Map.TileHeights[actor.Position.X, actor.Position.Y], 1);

                    if (actor.Position.Equals(actor.TargetPoint))
                        UpdateUserStatus(actor, room.Grid.ItemAt(actor.Position));

                    actor.UpdateNeeded = true;
                    actor.SetStep = false;

                    if (actor.QuitRoom)
                    {
                        actor.QuitRoom = false;
                        room.RemoveActor(actor, true);
                    }
                }

                if (actor.CalcPath)
                {
                    if (actor.IsWalking)
                        actor.Path.Clear();

                    actor.Path = Pathfinder.Pathfinder.GetPath(room, actor.Position, actor.TargetPoint, actor);

                    if (actor.IsWalking)
                    {
                        actor.StepsOnPath = 1;
                        actor.CalcPath = false;
                    }
                    else
                    {
                        actor.CalcPath = false;
                        actor.Path.Clear();
                    }
                }

                if (actor.IsWalking)
                {
                    if ((actor.StepsOnPath >= actor.Path.Count))
                    {
                        actor.Path.Clear();
                        actor.CalcPath = false;
                        actor.Statusses.Remove("mv");
                    }
                    else
                    {
                        Point2D nextStep = actor.Path[(actor.Path.Count - actor.StepsOnPath) - 1];
                        actor.StepsOnPath++;

                        actor.Statusses.Remove("mv");
                        if (actor.Statusses.ContainsKey("sit"))
                            actor.Statusses.Remove("sit");
                        actor.Statusses.Add("mv", $"{nextStep.X},{nextStep.Y},{Math.Round(room.Map.TileHeights[nextStep.X, nextStep.Y], 1)}");

                        actor.Rotation = Pathfinder.Pathfinder.CalculateRotation(actor.Position.X, actor.Position.Y, nextStep.X,
                            nextStep.Y);
                        actor.NextTile = nextStep;

                        actor.SetStep = true;
                        actor.UpdateNeeded = true;
                    }
                }
                else
                {
                    if (actor.Statusses.ContainsKey("mv"))
                    {
                        actor.Statusses.Remove("mv");
                        actor.UpdateNeeded = true;
                    }
                }

                #endregion

                if (!actor.UpdateNeeded || toUpdate.Contains(actor))
                    continue;

                actor.UpdateNeeded = false;
                toUpdate.Add(actor);
            }
            if (toUpdate.Count > 0)
                room.SendComposer(new UserUpdateMessageComposer(toUpdate));
        }

        private void UpdateUserStatus(RoomActor actor, Item item)
        {
            if (item != null)
            {
                item.ActorOnItem = actor;
                if (item.Definition.ItemType == "seat")
                {
                    if (!actor.Statusses.ContainsKey("sit"))
                        actor.Statusses.Add("sit", "1.00");
                    actor.Rotation = item.Rotation;
                }
            }
        }

        public void Dispose()
        {
            _wtoken.Cancel();
            _task = null;
            _wtoken = null;
        }
    }
}