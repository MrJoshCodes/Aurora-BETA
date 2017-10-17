using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessActor
    {
        public void Process(RoomActor actor, Room room)
        {
            if (actor.SetStep)
            {
                room.Grid.PointAt(actor.Position.X, actor.Position.Y).Actors.Remove(actor);
                room.Grid.PointAt(actor.NextTile.X, actor.NextTile.Y).Actors.Add(actor);
                
                actor.Position = actor.NextTile;
                actor.Position.Z = Math.Round(room.Grid.TileHeight(actor.Position), 1);

                if (actor.Position.Equals(actor.TargetPoint))
                    UpdateUserStatus(actor, room.Grid.ItemsAt(actor.Position));

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

                if (!room.Grid.ValidStep(actor.TargetPoint.X, actor.TargetPoint.Y, actor, false)) 
                {
                    actor.CalcPath = false;
                    return;
                }

                actor.Path = Pathfinder.Pathfinder.GetPath(room, actor.Position, actor.TargetPoint, actor);

                if (actor.IsWalking)
                {
                    actor.StepsOnPath = 1;
                }
                else
                {
                    actor.Path = Pathfinder.Pathfinder.GetPath(room, actor.Position, actor.TargetPoint, actor, true);
                    if (actor.IsWalking)
                    {
                        actor.StepsOnPath = 1;
                    }
                    else
                    {
                        actor.Path.Clear();
                    }
                }
                actor.CalcPath = false;
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
                    actor.Statusses.Add("mv", $"{nextStep.X},{nextStep.Y},{Math.Round(room.Grid.TileHeight(nextStep), 1)}");

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
        }

        private void UpdateUserStatus(RoomActor actor, List<Item> items)
        {
            if (items.Count > 0)
            {
                Item highestItem = items[items.Count - 1];
                highestItem.ActorOnItem = actor;
                if (highestItem.Definition.ItemType == "seat")
                {
                    if (!actor.Statusses.ContainsKey("sit"))
                        actor.Statusses.Add("sit", (highestItem.Definition.Height).ToString());
                    actor.Rotation = highestItem.Rotation;
                }
            }
        }
    }
}
