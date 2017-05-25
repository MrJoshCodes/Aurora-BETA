using AuroraEmu.Database;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace AuroraEmu.Game.Items
{
    public class ItemController
    {
        private ItemController instance;

        public Dictionary<int, Item> Items { get; private set; }

        public ItemController()
        {
            using (ISession session = DatabaseHelper.GetInstance().SessionFactory.OpenSession())
            {
                Items = session.CreateCriteria<Item>().List<Item>().ToDictionary(x => x.Id);
            }
        }

        public ItemController GetInstance()
        {
            if (instance == null)
                instance = new ItemController();

            return instance;
        }
    }
}
