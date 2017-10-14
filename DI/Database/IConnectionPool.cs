using AuroraEmu.Database;

namespace AuroraEmu.DI.Database
{
    public interface IConnectionPool
    {
        DatabaseConnection PopConnection();

        void ReturnConnection(DatabaseConnection con);
    }
}
