using AuroraEmu.DI.Game.Rooms;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AuroraEmu.Game.Rooms
{
    public class RoomController : IRoomController
    {
        public ConcurrentDictionary<int, Room> Rooms { get; set; }
        public Dictionary<string, RoomMap> RoomMaps { get; set; }

        public RoomController()
        {
            Rooms = new ConcurrentDictionary<int, Room>();
            RoomMaps = new Dictionary<string, RoomMap>();
            LoadRoomMaps();
        }

        public void LoadRoomMaps()
        {
            Engine.MainDI.RoomDao.LoadRoomMaps(RoomMaps);

            Engine.Logger.Info($"Loaded {RoomMaps.Count} room maps.");
        }

        public Room GetRoom(int id)
        {
            if (Rooms.TryGetValue(id, out Room room))
                return room;

            room = Engine.MainDI.RoomDao.GetRoom(id);
            return room;
        }

        public int GetUserRoomCount(int userId) =>
            Engine.MainDI.RoomDao.GetUserRoomCount(userId);

        public bool TryCreateRoom(string name, string model, int ownerId, out int roomId)
        {
            Room tmpRoom = new Room()
            {
                Name = name,
                Model = model,
                OwnerId = ownerId,
                Map = Engine.MainDI.RoomController.RoomMaps[model]
            };

            tmpRoom.Id = Engine.MainDI.RoomDao.GetRoomId(name, model, ownerId);

            if (tmpRoom.Id > 0)
            {
                roomId = tmpRoom.Id;
                return true;
            }

            roomId = -1;
            return false;
        }
    }
}