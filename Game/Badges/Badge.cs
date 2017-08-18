using System.Data;

namespace AuroraEmu.Game.Badges
{
    public class Badge
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public int Slot { get; private set; }

        public Badge(DataRow row)
        {
            Id = (int)row["id"];
            Code = (string)row["badge_code"];
            Slot = (int)row["slot_number"];
        }
    }
}
