using AuroraEmu.Game.Players;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerFriend
    {
        public string Username { get; set; }
        public string Motto { get; set; }
        public string Figure { get; set; }
        public int UserTwoId { get; set; }

        public MessengerFriend(DataRow row)
        {
            UserTwoId = (int) row["user_two_id"];
            Username = (string) row["username"];
            Motto = (string) row["motto"];
            Figure = (string) row["figure"];
        }

        public MessengerFriend(int id)
        {
            Player player = Engine.MainDI.PlayerController.GetPlayerById(id);
            Username = player.Username;
            Motto = player.Motto;
            UserTwoId = player.Id;
            Figure = player.Figure;
        }
    }
}