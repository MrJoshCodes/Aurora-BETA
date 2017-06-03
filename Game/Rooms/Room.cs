using AuroraEmu.Game.Players;
using System;
using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoomState State { get; set; }
        public int PlayersIn { get; set; }
        public int PlayersMax { get; set; }
        public int CategoryId { get; set; }
        public string Model { get; set; }
        public string CCTs { get; private set; }
        public bool ShowOwner { get; set; }
        public bool AllPlayerRights { get; set; }
        public string Icon { get; set; }
        public int Floor { get; set; }
        public int Wallpaper { get; set; }
        public double Landscape { get; set; }

        public Room()
        {
            State = RoomState.open;
            PlayersMax = 25;
            ShowOwner = true;
            AllPlayerRights = false;
            Icon = "HHIPAI";
            Landscape = 0.0;
        }
        public Room(DataRow row)
        {
            Id = (int)row["id"];
            OwnerId = row["owner_id"].GetType().Equals(typeof(DBNull)) ? 0 : (int)row["owner_id"];
            Name = (string)row["name"];
            Description = (string)row["description"];
            State = (RoomState)Enum.Parse(typeof(RoomState), (string)row["state"]);
            PlayersIn = (int)row["players_in"];
            PlayersMax = (int)row["players_max"];
            CategoryId = (int)row["category_id"];
            Model = (string)row["model"];
            CCTs = (string)row["ccts"];
            ShowOwner = (bool)row["show_owner"];
            AllPlayerRights = (bool)row["all_player_rights"];
            Icon = (string)row["icon"];
            Floor = (int)row["floor"];
            Wallpaper = (int)row["wallpaper"];
            Landscape = (double)row["landscape"];
        }

        public string Owner
        {
            get
            {
                return PlayerController.GetInstance().GetPlayerNameById(OwnerId);
            }
        }
        
        public int GetStateNumber()
        {
            return (int)State;
        }
    }
}
