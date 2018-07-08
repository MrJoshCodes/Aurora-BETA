﻿using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Players.Models;

namespace AuroraEmu.Database.DAO
{
    public class PlayerDao : IPlayerDao
    {
        public Player GetPlayerById(int id)
        {
            Player player = null;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery(
                    "SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket, favourite_group FROM players WHERE id = @id;");
                dbConnection.AddParameter("@id", id);
                using (var reader = dbConnection.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }
            
            return player;
        }

        public Player GetPlayerBySSO(string sso)
        {
            Player player = null;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket, favourite_group FROM players WHERE sso_ticket = @sso_ticket;");
                dbConnection.AddParameter("@sso_ticket", sso);
                using (var reader = dbConnection.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }

            return player;
        }

        public string GetPlayerNameById(int id, out string name)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT username FROM players WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                name = dbConnection.GetString();
            }
            
            return name;
        }

        public Player GetPlayerByName(string username)
        {
            Player player = null;
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("SELECT id, username, password, email, gender, figure, motto, coins, pixels, rank, home_room, block_friendrequests, sso_ticket, favourite_group FROM players WHERE username = @username;");
                dbConnection.AddParameter("@username", username);
                using (var reader = dbConnection.ExecuteReader())
                    if (reader.Read())
                        player = new Player(reader);
            }
            return player;
        }

        public void UpdateCurrency(int playerId, int amount, string type)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery($"UPDATE players SET {type} = @amount WHERE id = @playerId");
                dbConnection.AddParameter("@amount", amount);
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.Execute();
            }
        }

        public void UpdateHomeRoom(int playerId, int homeRoom)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE players SET home_room = @homeRoom WHERE id = @playerId");
                dbConnection.AddParameter("@homeRoom", homeRoom);
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.Execute();
            }
        }

        public void UpdateAppearance(int playerId, string figure, string gender, string motto)
        {
            using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
            {
                dbConnection.SetQuery("UPDATE players SET figure = @figure, gender = @gender, motto = @motto WHERE id = @playerId");
                dbConnection.AddParameter("@figure", figure);
                dbConnection.AddParameter("@gender", gender);
                dbConnection.AddParameter("@motto", motto);
                dbConnection.AddParameter("@playerId", playerId);
                dbConnection.Execute();
            }
        }
    }
}