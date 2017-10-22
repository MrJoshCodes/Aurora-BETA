using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Badges.Models;

namespace AuroraEmu.Database.DAO
{
    public class BadgesDao : IBadgesDao
    {
        public Dictionary<int, Badge> GetBadges(int playerId)
        {
            Dictionary<int, Badge> badges = new Dictionary<int, Badge>();

            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT * FROM `player_badges` WHERE `player_id` = @playerId");
                dbConnection.AddParameter("@playerId", playerId);
                using (var reader = dbConnection.ExecuteReader())
                    while (reader.Read())
                        badges.Add(reader.GetInt32("id"), new Badge(reader));
            }

            return badges;
        }

        public void ClearBadgeSlots(int playerId)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE `player_badges` SET `slot_number` = 0 WHERE `player_id` = @playerId");
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.Execute();
            }
        }

        public void UpdateBadgeSlots(int playerId, List<(int, int)> badges)
        {

            foreach ((int, int) badge in badges)
            {
                using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
                {
                    dbConnection.SetQuery("UPDATE `player_badges` SET `slot_number` = @slotNumber WHERE `id` = @badgeId AND `player_id` = @playerId LIMIT 1");
                    dbConnection.AddParameter("@slotNumber", badge.Item2);
                    dbConnection.AddParameter("@badgeId", badge.Item1);
                    dbConnection.AddParameter("@playerId", playerId);
                    dbConnection.Execute();
                }
            }
        }

        public int InsertBadge(int playerId, string code)
        {
            int id;
            
            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("INSERT INTO `player_badges` VALUES (NULL, @playerId, @code, 0)");
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.AddParameter("@code", code);
                id = dbConnection.Insert();
            }

            return id;
        }

        public void UpdateBadge(int id, string code)
        {
            using (var dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE `player_badges` SET `badge_code` = @code WHERE `id` = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                dbConnection.AddParameter("@code", code);
                dbConnection.Execute();
            }
        }
    }
}