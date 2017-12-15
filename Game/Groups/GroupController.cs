using AuroraEmu.DI.Database.DAO;
using AuroraEmu.DI.Game.Groups;

namespace AuroraEmu.Game.Groups
{
    public class GroupController : IGroupController
    {
        public IGroupDao Dao { get; }

        public GroupController(IGroupDao groupDao)
        {
            Dao = groupDao;
        }
    }
}