using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Badges;

namespace AuroraEmu.Database.DAO
{
    public class BadgesDao : IBadgesDao
    {
        public Dictionary<int, Badge> GetBadges(int playerId)
        {
            Dictionary<int, Badge> badges = new Dictionary<int, Badge>();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM `player_badges` WHERE `player_id` = @playerId");
                dbConnection.AddParameter("@playerId", playerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        badges.Add(reader.GetInt32("id"), new Badge(reader));

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }

            return badges;
        }
    }
}