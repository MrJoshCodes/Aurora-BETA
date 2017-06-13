using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace AuroraEmu.Game.Wordfilter
{
    public class WordfilterController
    {
        private static WordfilterController instance;
        private List<Wordfilter> filteredWords;

        public WordfilterController()
        {
            filteredWords = new List<Wordfilter>();
            Init();
        }

        public void Init()
        {
            using(DatabaseConnection dbClient = DatabaseManager.GetInstance().GetConnection())
            {
                dbClient.SetQuery("SELECT * FROM wordfilter");
                dbClient.Open();
                DataTable data = dbClient.GetTable();

                foreach(DataRow row in data.Rows)
                {
                    filteredWords.Add(new Wordfilter(row));
                }
            }
        }

        public string CheckString(string message)
        {
            foreach(Wordfilter filter in filteredWords.ToList())
            {
                if (message.ToLower().Contains(filter.Word) || message == filter.Word)
                {
                    message = Regex.Replace(message, filter.Word, filter.ReplacementWord, RegexOptions.IgnoreCase);
                }
            }
            return message;
        }

        public static WordfilterController GetInstance()
        {
            if (instance == null)
                instance = new WordfilterController();
            return instance;
        }
    }
}
