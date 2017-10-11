using AuroraEmu.DI.Database.DAO;
using System.Collections.Generic;
using AuroraEmu.Game.Wordfilter.Models;

namespace AuroraEmu.Database.DAO
{
    public class WordfilterDao : IWordfilterDao
    {
        public void WordfilterData(List<Wordfilter> wordFilter)
        {
            wordFilter.Clear();

            using (DatabaseConnection dbConnection = Engine.MainDI.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM wordfilter");
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        wordFilter.Add(new Wordfilter(reader));
            }
        }
    }
}