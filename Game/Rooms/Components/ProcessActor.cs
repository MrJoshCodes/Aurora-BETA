using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using System;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessActor
    {
        public void Process(RoomActor actor, Room room)
        {
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
    }
}
