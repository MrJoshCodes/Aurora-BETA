using AuroraEmu.Database;
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

        public Player(DataRow Row)
        {
            this.Id = (int)Row["id"];
            this.Username = (string)Row["username"];
            this.Email = (string)Row["email"];
            this.Gender = (string)Row["gender"];
            this.Figure = (string)Row["figure"];
            this.Motto = (string)Row["motto"];
            this.Coins = (int)Row["coins"];
            this.Pixels = (int)Row["pixels"];
            this.Rank = (byte)Row["rank"];
            this.HomeRoom = (int)Row["home_room"];
            this.SSO = (string)Row["sso_ticket"];
        }
    }
}
