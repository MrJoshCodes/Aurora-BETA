using MySql.Data.MySqlClient;
using System;

namespace AuroraEmu.Game.Catalog.Voucher
{
    public class Voucher
    {
        public int Amount { get; set; }
        public VoucherType Type { get; set; }

        public Voucher(MySqlDataReader reader)
        {
            Amount = reader.GetInt32("reward");
            Type = (VoucherType)Enum.Parse(typeof(VoucherType), reader.GetString("type"));
        }
    }
}
