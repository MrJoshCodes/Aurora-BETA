using AuroraEmu.DI.Game.Commands;
using AuroraEmu.Game.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AuroraEmu.Game.Commands
{
    public class CommandController : ICommandController
    {
        private Dictionary<string, ICommand> _commands;

        public CommandController()
        {
            // TODO: Make this less-static?

            _commands = new Dictionary<string, ICommand>();

            foreach (Type type in Assembly.GetEntryAssembly().GetTypes())
            {
                if (type.GetInterfaces().Contains(typeof(ICommand)))
                {
                    ICommand command = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as ICommand;

                    foreach (string header in command.CommandHeaders)
                    {
                        _commands.Add(header, command);
                    }
                }
            }

            Engine.Logger.Info($"Loaded a total of {_commands.Count} chat commands.");
        }

        public bool TryHandleCommand(Client client, string input)
        {
            string[] data = input.Split(' ');

            string header = data[0];

            if (!_commands.TryGetValue(header, out ICommand command))
                return false;

            try
            {
                command.Invoke(client, data.Skip(1).ToArray());

                return true;
            }
            catch (Exception ex)
            {
                Engine.Logger.Error(ex);

                return false;
            }
        }
    }
}
