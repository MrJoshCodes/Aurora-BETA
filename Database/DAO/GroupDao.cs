using AuroraEmu.DI.Database.DAO;
using AuroraEmu.Game.Groups.Models;

namespace AuroraEmu.Database.DAO
{
    public class GroupDao : IGroupDao
    {
        public Group GetGroup(int id)
        {
            Group group = null;
            
            using (var connection = Engine.Locator.ConnectionPool.PopConnection())
            {
                connection.SetQuery("SELECT id, name, description, badge FROM groups WHERE id = @group_id LIMIT 1");
                connection.AddParameter("@group_id", id);

                using (var reader = connection.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        group = new Group(reader);
                    }    
                }
            }

            return group;
        }
    }
}