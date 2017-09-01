﻿using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Badges
{
    public class Badge
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public int Slot { get; private set; }

        public Badge(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Code = reader.GetString("badge_code");
            Slot = reader.GetInt32("slot_number");
        }
    }
}