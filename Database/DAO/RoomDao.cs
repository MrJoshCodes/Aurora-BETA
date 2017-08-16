using System.Data;
using AuroraEmu.DI.Database.DAO;

namespace AuroraEmu.Database.DAO
{
    public class RoomDao : IRoomDao
    {
        public DataTable LoadRoomMaps()
        {
            DataTable table;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM room_maps");
                table = dbConnection.GetTable();

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            return table;
        }

        public DataRow GetRoom(int id)
        {
            DataRow row;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT * FROM rooms WHERE id = @id LIMIT 1");
                dbConnection.AddParameter("@id", id);
                row = dbConnection.GetRow();

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            return row;
        }

        public int GetUserRoomCount(int userId)
        {
            int roomCount;
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("SELECT COUNT(*) FROM rooms WHERE owner_id = @ownerId");
                dbConnection.AddParameter("@ownerId", userId);
                roomCount = int.Parse(dbConnection.GetString());

                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            return roomCount;
        }

        public int GetRoomId(string name, string model, int ownerId)
        {
            int tmpRoomId;
            
            using (DatabaseConnection dbConnection = Engine.MainDI.DatabaseController.GetConnection())
            {
                dbConnection.Open();
                dbConnection.SetQuery("INSERT INTO rooms (owner_id,name,model) VALUES (@ownerId, @name, @model)");
                dbConnection.AddParameter("@ownerId", ownerId);
                dbConnection.AddParameter("@name", name);
                dbConnection.AddParameter("@model", model);

                tmpRoomId = dbConnection.Insert();
                dbConnection.BeginTransaction();
                dbConnection.Commit();
                dbConnection.Dispose();
            }
            
            return tmpRoomId;
        }
    }
}