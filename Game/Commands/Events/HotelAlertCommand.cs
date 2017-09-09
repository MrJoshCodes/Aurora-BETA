using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets;

namespace AuroraEmu.Game.Commands.Events
{
    public class HotelAlertCommand : ICommand
    {
        public string[] CommandHeaders
        {
            get
            {
                return new[] { "ha", "hotelalert" };
            }
        }

        public void Invoke(Client client, params object[] args)
        {
            MessageComposer composer = new MessageComposer(139);
            composer.AppendString(string.Join(" ", args) + "\r\n - " + client.Player.Username);

            foreach (Client target in Engine.MainDI.ClientController.Clients.Values)
            {
                target.SendComposer(composer);
            }
        }
    }
}