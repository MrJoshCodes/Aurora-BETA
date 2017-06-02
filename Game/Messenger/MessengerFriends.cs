using AuroraEmu.Game.Players;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerFriends
    {
        public string Username { get; set; }
        public string Motto { get; set; }
        public string Figure { get; set; }
        public int UserTwoId { get; set; }

        public MessengerFriends(DataRow Row)
        {
            UserTwoId = (int)Row["user_two_id"];
            Username = (string)Row["username"];
            Motto = (string)Row["motto"];
            Figure = (string)Row["figure"];
        }

        public bool IfOnline()
        {
            return PlayerController.GetInstance().PlayerIsOnline(UserTwoId);
        }
    }
}
