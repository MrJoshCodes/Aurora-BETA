namespace AuroraEmu.Game.Players
{
    public class Player
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Figure { get; set; }
        public virtual string Motto { get; set; }
        public virtual int Coins { get; set; }
        public virtual int Pixels { get; set; }
        public virtual byte Rank { get; set; }
        public virtual int HomeRoom { get; set; }
        public virtual string SSO { get; set; }
    }
}
