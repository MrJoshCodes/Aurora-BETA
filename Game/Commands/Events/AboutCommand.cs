using System.Linq;
using System.Text;
using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Misc;

namespace AuroraEmu.Game.Commands.Events
{
    public class AboutCommand : ICommand
    {
        public string[] CommandHeaders => new[] {"about", "info"};
        public void Invoke(Client client, params object[] args)
        {
            var aboutBuilder = new StringBuilder();
            aboutBuilder.Append("AuroraEmu v1.0 ALPHA\n\n");
            aboutBuilder.Append("Credits:\n");
            aboutBuilder.Append("Josh - Lead Aurora Developer\n");
            aboutBuilder.Append("Spreedblood - Aurora Developer\n");
            aboutBuilder.Append("Quackster - Some fixes, rollers, catalogue converter and public room item inserter\n\n");
            aboutBuilder.Append($"Players online: {Engine.Locator.ClientController.Clients.Count(c => c.Value.Player != null)}\n");
            aboutBuilder.Append($"Rooms active: {Engine.Locator.RoomController.Rooms.Values.Count(r => r.Active && r.Actors.Count > 0)}");
            client.SendComposer(new HabboBroadcastMessageComposer(aboutBuilder.ToString()));
        }
    }
}