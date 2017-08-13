using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Network.Game.Packets.Composers.Users;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessComponent
    {
        private Room room;
        private CancellationTokenSource wtoken;
        private ActionBlock<DateTimeOffset> task;
        private DateTime lastLoopExecution;

        public ProcessComponent(Room room)
        {
            this.room = room;
        }

        public void SetupRoomLoop()
        {
            wtoken = new CancellationTokenSource();

            task = TaskController.GetInstance().ExecutePeriodic(now => Loop(), wtoken.Token, 25);
            task.Post(DateTimeOffset.Now);
        }

        private void Loop()
        {

            TimeSpan timeSinceLastLoop = DateTime.Now - lastLoopExecution;
            if (timeSinceLastLoop.Milliseconds >= 500)
            {
                lastLoopExecution = DateTime.Now;

                room.Loop();

                List<RoomActor> toUpdate = new List<RoomActor>();
                foreach (RoomActor actor in room.Actors.Values)
                {
                    #region walking related
                    if (actor.SetStep)
                    {
                        actor.X = actor.SetX;
                        actor.Y = actor.SetY;

                        actor.UpdateNeeded = true;

                        actor.SetStep = false;
                    }

                    if (actor.CalcPath)
                    {
                        if (actor.IsWalking)
                            actor.Path.Clear();

                        actor.Path = Pathfinder.Pathfinder.FindPath(room, actor.Client.CurrentRoom.Map, new Point2D(actor.X, actor.Y), new Point2D(actor.TargetX, actor.TargetY));

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

                            int nextX = nextStep.X;
                            int nextY = nextStep.Y;
                            int rot = Pathfinder.Pathfinder.CalculateRotation(actor.X, actor.Y, nextX, nextY, false);

                            actor.Statusses.Remove("mv");

                            actor.Statusses.Add("mv", $"{nextX},{nextY},0.00");
                            actor.SetStep = true;
                            actor.SetX = nextX;
                            actor.SetY = nextY;
                            actor.Rotation = rot;
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
        }
    }
}
