using AuroraEmu.Game.Players.Components;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Players
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Figure { get; set; }
        public string Motto { get; set; }
        public int Coins { get; set; }
        public int Pixels { get; set; }
        public byte Rank { get; set; }
        public int HomeRoom { get; set; }
        public string SSO { get; set; }
        public int BlockNewFriends { get; set; }

        public MessengerComponent MessengerComponent { get; set; }
        public BadgesComponent BadgesComponent { get; set; }

        public Player(MySqlDataReader reader)
        {
            reader.Read();
            Id = reader.GetInt32("id");
            Username = reader.GetString("username");
            Email = reader.GetString("email");
            Gender = reader.GetString("gender");
            Figure = reader.GetString("figure");
            Motto = reader.GetString("motto");
            Coins = reader.GetInt32("coins");
            Pixels = reader.GetInt32("pixels");
            Rank = reader.GetByte("rank");
            HomeRoom = reader.GetInt32("home_room");
            SSO = reader.GetString("sso_ticket");
            BlockNewFriends = reader.GetInt32("block_friendrequests");
        }
    }
}