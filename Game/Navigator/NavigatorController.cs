using AuroraEmu.Database;
using NHibernate;
using System.Collections.Generic;

namespace AuroraEmu.Game.Navigator
{
    public class NavigatorController
    {
        private static NavigatorController instance;

        public IList<FrontpageItem> FrontpageItems { get; private set; }

        public NavigatorController()
        {
            using (ISession session = DatabaseHelper.GetInstance().SessionFactory.OpenSession())
            {
                FrontpageItems = session.CreateCriteria<FrontpageItem>().List<FrontpageItem>();
            }

            Engine.Logger.Info($"Loaded {FrontpageItems.Count} navigator frontpage items.");
        }

        public static NavigatorController GetInstance()
        {
            if (instance == null)
                instance = new NavigatorController();

            return instance;
        }
    }
}
