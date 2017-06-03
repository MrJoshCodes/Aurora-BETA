using AuroraEmu.Database;
using System.Collections.Concurrent;
using System.Data;

namespace AuroraEmu.Game.Rooms
{
    public class RoomController
    {
        private static RoomController instance;

        public ConcurrentDictionary<int, Room> Rooms { get; private set; }

        public RoomController()
        {
            Rooms = new ConcurrentDictionary<int, Room>();
        }

        public Room GetRoom(int id)
        {
            if (Rooms.TryGetValue(id, out Room room))
                return room;

            DataRow row;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM rooms WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                dbConnection.Open();

                row = dbConnection.GetRow();
            }

            if (row != null)
            {
                room = new Room(row);
                Rooms.TryAdd(id, room);

                return room;
            }

            return null;
        }

        public static RoomController GetInstance()
        {
            if (instance == null)
                instance = new RoomController();

            return instance;
        }
    }
}
