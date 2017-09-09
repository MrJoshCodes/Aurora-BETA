using AuroraEmu.Game.Clients;

namespace AuroraEmu.Game.Commands
{
    public interface ICommand
    {
        string[] CommandHeaders { get; }
        void Invoke(Client client, params object[] args);
    }
}