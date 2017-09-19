using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Items;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Items
{
    public class Item
    {
        private ItemDefinition _definition;

        public int Id { get; set; }
        public int RoomId { get; set; }
        public int OwnerId { get; set; }
        public int DefinitionId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Z { get; set; }
        public int Rotation { get; set; }
        public string Data { get; set; }
        public string Wallposition { get; set; }
        public bool Cycling { get; private set; } = false;

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
            X = reader.GetInt32("x");
            Y = reader.GetInt32("y");
            Z = reader.GetInt32("z");
            Rotation = reader.GetInt32("rotation");
            Data = reader.GetString("data");
            Wallposition = reader.GetString("wallposition");
        }

        public ItemDefinition Definition
        {
            get
            {
                if (_definition == null)
                    _definition = Engine.MainDI.ItemController.GetTemplate(DefinitionId);

                return _definition;
            }
        }

        public IItemHandler Handler {
            get {
                if(Engine.MainDI.ItemController.Handlers.TryGetValue(Definition.HandleType, out IItemHandler handler))
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
                if (Engine.MainDI.ItemController.Handlers.TryGetValue(Definition.HandleType, out IItemHandler itemHandler))
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
