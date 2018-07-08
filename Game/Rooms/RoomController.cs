using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game.Rooms;
using AuroraEmu.Game.Rooms.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public class RoomController : IRoomController
    {
        public ConcurrentDictionary<int, Room> Rooms { get; set; }
        public Dictionary<string, RoomMap> RoomMaps { get; set; }
        public IRoomDao Dao { get; }

        public RoomController(IRoomDao dao)
        {
            Dao = dao;
            Rooms = new ConcurrentDictionary<int, Room>();
            RoomMaps = new Dictionary<string, RoomMap>();
            LoadRoomMaps();
        }

        public void LoadRoomMaps()
        {
            Dao.LoadRoomMaps(RoomMaps);

            Engine.Logger.Info($"Loaded {RoomMaps.Count} room maps.");
        }

        public Room GetRoom(int id)
        {
            if (Rooms.TryGetValue(id, out Room room))
                return room;

            room = Dao.GetRoom(id);
            return room;
        }

        public int GetUserRoomCount(int userId) =>
            Dao.GetUserRoomCount(userId);

        public bool TryCreateRoom(string name, string model, int ownerId, out int roomId)
        {
            System.Console.WriteLine(model);
            Room tmpRoom = new Room()
            {
                Name = name,
                Model = model,
                OwnerId = ownerId,
                Map = Engine.Locator.RoomController.RoomMaps[model]
            };

            tmpRoom.Id = Dao.GetRoomId(name, model, ownerId);

            if (tmpRoom.Id > 0)
            {
                roomId = tmpRoom.Id;
                return true;
            }

            roomId = -1;
            return false;
        }

        public List<string> GetRoomTags(int roomId)
        {
            return Dao.GetRoomTags(roomId);
        }
    }
}