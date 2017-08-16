using AuroraEmu.Database;

namespace AuroraEmu.DI.Database
{
    public interface IDatabaseController
    {
        void Init(string connectionString);

        bool TryConnection();

        DatabaseConnection GetConnection();
    }
}
