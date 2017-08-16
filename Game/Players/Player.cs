using System.Data;
using AuroraEmu.Game.Players.Components;

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

        public Player(DataRow row)
        {
            Id = (int) row["id"];
            Username = (string) row["username"];
            Email = (string) row["email"];
            Gender = (string) row["gender"];
            Figure = (string) row["figure"];
            Motto = (string) row["motto"];
            Coins = (int) row["coins"];
            Pixels = (int) row["pixels"];
            Rank = (byte) row["rank"];
            HomeRoom = (int) row["home_room"];
            SSO = (string) row["sso_ticket"];
            BlockNewFriends = (int) row["block_friendrequests"];
            MessengerComponent = new MessengerComponent(this);
        }
    }
}