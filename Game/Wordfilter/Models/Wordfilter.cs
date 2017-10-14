using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Wordfilter.Models
{
    public class Wordfilter
    {
        public string Word { get; set; }
        public string ReplacementWord { get; set; }

        public Wordfilter(MySqlDataReader reader)
        {
            Word = reader.GetString("not_allowed_message");
            ReplacementWord = reader.GetString("replace_message");
        }
    }
}