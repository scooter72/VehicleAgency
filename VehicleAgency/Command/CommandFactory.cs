using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Command
{
    public sealed class CommandFactory
    {

        public static T ExecuteCommand<T>(CommandTypes type, CommandContext context)
        {
            switch (type)
            {
                case CommandTypes.Exit:
                    break;
                case CommandTypes.InputVehicleData:
                    break;
                case CommandTypes.RemoveVehicle:
                    return (T)(new RemoveVehicle()).Execute(context);
                case CommandTypes.PrintVehiclesToScreen:
                    return (T)(new PrintToScreent()).Execute(context);
                case CommandTypes.SaveVehiclesToFile:
                    break;
                case CommandTypes.LoadVehiclesFromFile:
                    break;
                case CommandTypes.GetLatetstVehicleEntry:
                    break;
                case CommandTypes.SortVehicles:
                    break;
                case CommandTypes.SearchForVehicles:
                    break;
                default:
                    break;
            }
            return default(T);
        }
    }
}
