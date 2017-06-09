using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class RoomCategory
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int MinRank { get; private set; }
        public bool TradeAllowed { get; private set; }

        public int PlayersInside { get; set; }

        public RoomCategory(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            MinRank = (int)row["min_rank"];
            TradeAllowed = (bool)row["trade_allowed"];
        }
    }
}
