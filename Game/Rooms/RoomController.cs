using AuroraEmu.DI.Game.Rooms;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

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
            RoomMaps.Clear();

            DataTable table = Engine.MainDI.RoomDao.LoadRoomMaps();

            foreach (DataRow row in table.Rows)
            {
                RoomMaps.Add((string) row["name"], new RoomMap(row));
            }

            Engine.Logger.Info($"Loaded {RoomMaps.Count} room maps.");
        }

        public Room GetRoom(int id)
        {
            if (Rooms.TryGetValue(id, out Room room))
                return room;

            DataRow row = Engine.MainDI.RoomDao.GetRoom(id);

            if (row != null)
            {
                room = new Room(row);
                Rooms.TryAdd(id, room);

                return room;
            }

            return null;
        }

        public int GetUserRoomCount(int userId)
        {
            return Engine.MainDI.RoomDao.GetUserRoomCount(userId);
        }

        public bool TryCreateRoom(string name, string model, int ownerId, out Room room)
        {
            Room tmpRoom = new Room()
            {
                Name = name,
                Model = model,
                OwnerId = ownerId
            };

            tmpRoom.Id = Engine.MainDI.RoomDao.GetRoomId(name, model, ownerId);

            if (tmpRoom.Id > 0)
            {
                Rooms.TryAdd(tmpRoom.Id, tmpRoom);
                room = tmpRoom;
                return true;
            }

            room = null;
            return false;
        }
    }
}