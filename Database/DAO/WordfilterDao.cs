using System.Data;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Database.DAO
{
    public class WordfilterDao : IWordfilterDao
    {
        public DataTable WordfilterData()
        {
            DataTable data;
            using (DatabaseConnection dbClient = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbClient.Open();
                dbClient.SetQuery("SELECT * FROM wordfilter");
                data = dbClient.GetTable();
                
                dbClient.BeginTransaction();
                dbClient.Commit();
                dbClient.Dispose();
            }
            return data;
        }
    }
}