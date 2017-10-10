using AuroraEmu.Game.Players;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerFriend
    {
        public int UserTwoId { get; set; }
        public string Username { get; set; }
        public string Motto { get; set; }
        public string Figure { get; set; }

        public MessengerFriend(MySqlDataReader reader)
        {
            UserTwoId = reader.GetInt32("user_two_id");
            Username = reader.GetString("username");
            Motto = reader.GetString("motto");
            Figure = reader.GetString("figure");
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