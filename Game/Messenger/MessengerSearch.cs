using AuroraEmu.Game.Clients;
using MySql.Data.MySqlClient;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerSearch
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Figure { get; set; }
        public string Motto { get; set; }

        public Client Client() =>
            Engine.MainDI.ClientController.GetClientByHabbo(Id);
        
        public MessengerSearch(MySqlDataReader reader)
        {
            Id = reader.GetInt32("id");
            Username = reader.GetString("username");
            Figure = reader.GetString("figure");
            Motto = reader.GetString("motto");
        }
    }
}