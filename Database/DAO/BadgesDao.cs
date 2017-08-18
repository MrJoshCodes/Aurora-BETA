using System;
using System.Collections.Generic;
using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Badges;
using System.Data;

namespace AuroraEmu.Database.DAO
{
    public class BadgesDao : IBadgesDao
    {
        public Dictionary<int, Badge> GetBadges(int playerId)
        {
            DataTable table;
            Dictionary<int, Badge> badges = new Dictionary<int, Badge>();

            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM `player_badges` WHERE `player_id` = @playerId");
                dbConnection.AddParameter("@playerId", playerId);

                table = dbConnection.GetTable();
            }

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    badges.Add((int)row["id"], new Badge(row));
                }
            }

            return badges;
        }
    }
}
