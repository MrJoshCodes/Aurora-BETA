using AuroraEmu.Game.Clients;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Game.Rooms.Pathfinder;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Items.Models
{
    public class Item
    {
        private ItemDefinition _definition;

        public int Id { get; set; }
        public int RoomId { get; set; }
        public int OwnerId { get; set; }
        public int DefinitionId { get; set; }
        public int Rotation { get; set; }
        public string Data { get; set; }
        public string Wallposition { get; set; }
        public bool Cycling { get; private set; } = false;
        public RoomActor ActorOnItem { get; set; }
        public Point2D Position { get; set; }
        public List<Point2D> AffectedTiles =>
            Utilities.Extensions.AffectedTiles(Definition.Length, Definition.Width, Position.X, Position.Y, Rotation);
        public List<Point2D> Tiles {
            get {
                List<Point2D> affTiles = Utilities.Extensions.AffectedTiles(Definition.Length, Definition.Width, Position.X, Position.Y, Rotation);
                affTiles.Add(Position);
                return affTiles;
            }
        }

        public Item(int id, int ownerId, int definitionId, string data)
        {
            Id = id;
            OwnerId = ownerId;
            DefinitionId = definitionId;
            Data = data;
        }

        public Item(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            RoomId = reader.IsDBNull(1) ? -1 : reader.GetInt32("room_id");
            OwnerId = reader.GetInt32("owner_id");
            DefinitionId = reader.GetInt32("definition_id");
            Position = new Point2D(reader.GetInt32("x"), reader.GetInt32("y"), reader.GetInt32("z"));
            Rotation = reader.GetInt32("rotation");
            Data = reader.GetString("data");
            Wallposition = reader.GetString("wallposition");
        }

        public ItemDefinition Definition {
            get {
                if (_definition == null)
                    _definition = Engine.Locator.ItemController.GetTemplate(DefinitionId);

                return _definition;
            }
        }

        public IItemHandler Handler {
            get {
                if (Engine.Locator.ItemController.Handlers.TryGetValue(Definition.HandleType, out IItemHandler handler))
                {
                    return handler;
                }
                return null;
            }
        }

        public void ProcessItem(Client interactor, int cycles = 0)
        {
            Task.Run(async delegate
            {
                Cycling = true;
                await Task.Delay(cycles * 500);
                if (Engine.Locator.ItemController.Handlers.TryGetValue(Definition.HandleType, out IItemHandler itemHandler))
                {
                    itemHandler.Handle(this, interactor);
                }
                if (Definition.SpriteType == "i")
                    interactor.CurrentRoom.SendComposer(new ItemUpdateMessageComposer(this));
                else
                    interactor.CurrentRoom.SendComposer(new ObjectDataUpdateMessageComposer(Id, Data));
                Cycling = false;
            });
        }
    }
}