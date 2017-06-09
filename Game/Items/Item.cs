using System;
using System.Data;

namespace AuroraEmu.Game.Items
{
    public class Item
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int OwnerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Z { get; set; }
        public int Rotation { get; set; }
        public string Data { get; set; }
        public string Wallposition { get; set; }

        public Item(DataRow row)
        {
            Id = (int)row["id"];
            RoomId = row["room_id"].GetType().Equals(typeof(DBNull)) ? -1 : (int)row["room_id"];
            OwnerId = (int)row["owner_id"];
            X = (int)row["x"];
            Y = (int)row["y"];
            Z = (double)row["z"];
            Rotation = (int)row["rotation"];
            Data = (string)row["data"];
            Wallposition = (string)row["wallposition"];
        }
    }
}
