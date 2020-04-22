using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleAgency.Commands
{
    public sealed class CommandFactory
    {
        private static readonly List<ICommand> commands = new List<ICommand>(); 

        public static T ExecuteCommand<T>(Command type, CommandContext context)
        {
            if (commands.Count == 0)
            {
                LoadCommands();
            }
            return (T)(object)commands.First(c => c.GetCommand() == type).ExecuteCommand(context);
        }

        private static void LoadCommands()
        {
            commands.Add(new AddVehicle());
            commands.Add(new RemoveVehicle());
            commands.Add(new PrintVehiclesToScreen());
            commands.Add(new SaveVehiclesToFile());
            commands.Add(new GetLatestVehicleEntry());
            commands.Add(new SortVehicles());
            commands.Add(new SearchVehicles());
            commands.Add(new Exit());
        }
    }
}
