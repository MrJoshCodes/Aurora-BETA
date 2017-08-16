using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class RoomCategory
    {
        public int Id { get; }
        public string Name { get; }
        public int MinRank { get; }
        public bool TradeAllowed { get; }

        public int PlayersInside { get; set; }

        public RoomCategory(DataRow row)
        {
            Id = (int) row["id"];
            Name = (string) row["name"];
            MinRank = (int) row["min_rank"];
            TradeAllowed = (bool) row["trade_allowed"];
        }
    }
}