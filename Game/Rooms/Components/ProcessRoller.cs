using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using System;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessRoller
    {
        public void Process(List<Item> rollers, Room room)
        {
            List<Object> blacklist = new List<Object>();

            try 
            {
                foreach (Item roller in rollers) 
                {
                    List<Item> itemsAbove = room.Grid.ItemsAt(roller.Position.X, roller.Position.Y);

                    for (int i = 0; i < itemsAbove.Count; i++)//foreach (Item item in itemsAbove) 
                    {
                        Item item = itemsAbove[i];

                        if (blacklist.Contains(item))
                            return;
                        

                        if (item.Id == roller.Id)
                            continue;

                        if (item.Position.Z < roller.Position.Z) 
                            continue;

                        
                        Point2D nextPoint = roller.Position.GetSquareInFront(roller.Rotation);

                        if (!room.Grid.ValidPoint(nextPoint.X, nextPoint.Y)) 
                            continue;

                        List<Point2D> points = Utilities.Extensions.AffectedTiles(item.Definition.Length, item.Definition.Width, nextPoint.X, nextPoint.Y, item.Rotation);
                        points.Add(new Point2D(nextPoint.X, nextPoint.Y));
                        
                        foreach (Point2D point in points)
                            if (!room.Grid.ValidPoint(point))
                                continue;
                        
                        blacklist.Add(item);
                        double nextHeight = room.Grid.TileHeight(nextPoint);

                        room.SendComposer(new SlideObjectBundleMessageEvent(item.Id, item.Position.X, item.Position.Y, nextPoint.X, nextPoint.Y, roller.Id, item.Position.Z, nextHeight));
                        room.Grid.MoveItem(item, points);

                        item.Position.X = nextPoint.X;
                        item.Position.Y = nextPoint.Y;
                        item.Position.Z = nextHeight;

                        Engine.Locator.ItemController.Dao.UpdateItem(item.Id, item.Position.X, item.Position.Y, item.Position.Z, item.Rotation, room.Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);    
            }
        }
    }
}
