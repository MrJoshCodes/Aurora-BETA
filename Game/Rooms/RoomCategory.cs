using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Rooms
{
    public class RoomCategory
    {
        public int Id { get; }
        public int MinRank { get; }
        public int PlayersInside { get; set; }
        public bool TradeAllowed { get; }
        public string Name { get; }

        public RoomCategory(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Name = reader.GetString("name");
            MinRank = reader.GetInt32("min_rank");
            TradeAllowed = reader.GetBoolean("trade_allowed");
        }
    }
}