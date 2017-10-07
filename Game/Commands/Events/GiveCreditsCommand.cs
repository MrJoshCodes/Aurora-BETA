using AuroraEmu.Game.Clients;
using AuroraEmu.Network.Game.Packets.Composers.Misc;
using AuroraEmu.Game.Players;

namespace AuroraEmu.Game.Commands.Events
{
    public class GiveCreditsCommand : ICommand
    {
        public string[] CommandHeaders => new[] { "givecredits", "credits" };

        public void Invoke(Client client, params object[] args)
        {
            if (args.Length != 2)
            {
                client.SendComposer(new HabboBroadcastMessageComposer("Not enough arguments, correct usage: :givecredits/:credits <targetuser> <amount>"));
            }
            else
            {
                string targetUser = args[0].ToString();

                if (!int.TryParse(args[1].ToString(), out int amount))
                {
                    client.SendComposer(new HabboBroadcastMessageComposer("Cannot parse argument 2 to int."));
                    return;
                }

                Client target = Engine.MainDI.ClientController.GetClientByHabbo(targetUser);
                
                if (target != null)
                {
                    target.IncreaseCredits(amount);

                    target.SendComposer(new HabboBroadcastMessageComposer($"Staff member {client.Player.Username} gave you {amount} credits. Enjoy!"));
                }
                else
                {
                    Player player = Engine.MainDI.PlayerDao.GetPlayerByName(targetUser);

                    if (player != null)
                    {
                        Engine.MainDI.PlayerDao.UpdateCurrency(player.Id, +amount, "coins");
                    }
                }
            }
        }
    }
}
