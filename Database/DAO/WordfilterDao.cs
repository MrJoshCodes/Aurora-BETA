using AuroraEmu.DI.Database.DAO;
using System.Collections.Generic;
using AuroraEmu.Game.Wordfilter;

namespace AuroraEmu.Database.DAO
{
    public class WordfilterDao : IWordfilterDao
    {
        public List<Wordfilter> WordfilterData(List<Wordfilter> wordFilter)
        {
            wordFilter.Clear();

            using (DatabaseConnection dbClient = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbClient.SetQuery("SELECT * FROM wordfilter");
                using (var reader = dbClient.ExecuteReader())
                    while (reader.Read())
                        wordFilter.Add(new Wordfilter(reader));
            }
            return wordFilter;
        }
    }
}