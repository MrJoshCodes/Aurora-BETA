using AuroraEmu.Database;
using AuroraEmu.Game.Items.Models;
using AuroraEmu.Game.Rooms.Models;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessItems
    {
        public void Process(Room room)
        {
            foreach (Item item in room.ItemUpdates.Values)
            {
                using (DatabaseConnection dbConnection = Engine.Locator.ConnectionPool.PopConnection())
                {
                    dbConnection.SetQuery("UPDATE `items` SET `x` = @x, `y` = @y, `z` = @z, `rotation` = @rotation, `data` = @data, `wallposition` = @wallposition WHERE `id` = @id LIMIT 1");
                    dbConnection.AddParameter("@x", item.Position.X);
                    dbConnection.AddParameter("@y", item.Position.Y);
                    dbConnection.AddParameter("@z", item.Position.Z);
                    dbConnection.AddParameter("@data", item.Data);
                    dbConnection.AddParameter("@wallposition", item.Wallposition);
                    dbConnection.AddParameter("@id", item.Id);

                    room.ItemUpdates.TryRemove(item.Id, out Item _);
                }
            }
        }
    }
}