using AuroraEmu.Database;
using System.Collections.Generic;
using System.Data;

namespace AuroraEmu.Game.Navigator
{
    public class NavigatorController
    {
        private static NavigatorController instance;

        public List<FrontpageItem> FrontpageItems { get; private set; }

        public NavigatorController()
        {
            FrontpageItems = new List<FrontpageItem>();

            ReloadFrontpageItems();
        }

        public void ReloadFrontpageItems()
        {
            FrontpageItems.Clear();

            DataTable result;

            using (DatabaseConnection dbConnection = DatabaseManager.GetInstance().GetConnection())
            {
                dbConnection.SetQuery("SELECT * FROM frontpage_items;");
                dbConnection.Open();

                result = dbConnection.GetTable();
            }

            foreach(DataRow row in result.Rows)
            {
                FrontpageItems.Add(new Navigator.FrontpageItem(row));
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