using System.Data;

namespace AuroraEmu.Game.Wordfilter
{
    public class Wordfilter
    {
        public string Word { get; set; }
        public string ReplacementWord { get; set; }

        public Wordfilter(DataRow row)
        {
            Word = (string)row["not_allowed_message"];
            ReplacementWord = (string)row["replace_message"];
        }
    }
}
