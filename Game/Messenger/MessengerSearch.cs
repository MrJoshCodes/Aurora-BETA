using System.Data;
using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerSearch
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Figure { get; set; }
        public string Motto { get; set; }

        public Client Client()
        {
            return Engine.MainDI.ClientController.GetClientByHabbo(Id);
        }
        
        public MessengerSearch(DataRow row)
        {
            Id = (int) row["id"];
            Username = (string) row["username"];
            Figure = (string) row["figure"];
            Motto = (string) row["motto"];
        }
    }
}