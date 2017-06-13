using System.Data;

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

        public Player(DataRow Row)
        {
            Id = (int)Row["id"];
            Username = (string)Row["username"];
            Email = (string)Row["email"];
            Gender = (string)Row["gender"];
            Figure = (string)Row["figure"];
            Motto = (string)Row["motto"];
            Coins = (int)Row["coins"];
            Pixels = (int)Row["pixels"];
            Rank = (byte)Row["rank"];
            HomeRoom = (int)Row["home_room"];
            SSO = (string)Row["sso_ticket"];
            BlockNewFriends = (int)Row["block_friendrequests"];
        }
    }
}
