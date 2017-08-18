using System;
using System.Data;

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

        public Item(int id, int ownerId, int definitionId, string data)
        {
            Id = id;
            OwnerId = ownerId;
            DefinitionId = definitionId;
            Data = data;
        }

        public Item(DataRow row)
        {
            Id = (int)row["id"];
            RoomId = row["room_id"].GetType().Equals(typeof(DBNull)) ? -1 : (int)row["room_id"];
            OwnerId = (int)row["owner_id"];
            DefinitionId = (int)row["definition_id"];
            X = (int)row["x"];
            Y = (int)row["y"];
            Z = (double)row["z"];
            Rotation = (int)row["rotation"];
            Data = (string)row["data"];
            Wallposition = (string)row["wallposition"];
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
    }
}
