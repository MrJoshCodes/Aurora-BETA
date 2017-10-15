using System;
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
            var info = new StringBuilder();
            info.AppendLine("AuroraEmu v0.0.1 Alpha");
            info.Append("Copyright (C) 2017\r\nLord Glaceon & Spreed\r\n");
            info.Append("Thanks to:\r\n");
            info.Append("Meth0d - UberEmu for some packets");
            info.Append("Spreedblood - For a bunch of help with the emulator");
            info.Append("Quackster - A few things");
            info.Append("\r\n");
            info.Append("Current sessions connected: " + Engine.Locator.ClientController.Clients.Count);

            client.SendComposer(new HabboBroadcastMessageComposer(info.ToString()));
        }
    }
}