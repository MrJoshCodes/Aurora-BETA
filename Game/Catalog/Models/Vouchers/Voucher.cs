using MySql.Data.MySqlClient;
using System;

namespace AuroraEmu.Game.Catalog.Models.Vouchers
{
    public class Voucher
    {
        public string Reward { get; set; }
        public VoucherType Type { get; set; }

        public Voucher(MySqlDataReader reader)
        {
            Reward = reader.GetString("reward");
            Type = (VoucherType)Enum.Parse(typeof(VoucherType), reader.GetString("type"));
        }
    }
}
