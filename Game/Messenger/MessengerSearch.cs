using AuroraEmu.Network.Game.Packets;
using System.Data;

namespace AuroraEmu.Game.Messenger
{
    public class MessengerSearch
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Figure { get; set; }
        public string Motto { get; set; }

        public MessengerSearch(DataRow Row)
        {
            Id = (int)Row["id"];
            Username = (string)Row["username"];
            Figure = (string)Row["figure"];
            Motto = (string)Row["motto"];
        }
    }
}
