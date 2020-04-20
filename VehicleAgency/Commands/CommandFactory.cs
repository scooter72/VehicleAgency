namespace VehicleAgency.Commands
{
    public sealed class CommandFactory
    {

        public static T ExecuteCommand<T>(Command type, CommandContext context)
        {
            switch (type)
            {
                case Command.Exit:
                    break;
                case Command.AddVehicle:
                    new AddVehicle().Execute(context);
                    break;
                case Command.RemoveVehicle:
                    new RemoveVehicle().Execute(context);
                    break;
                case Command.PrintVehiclesToScreen:
                    new PrintToScreent().Execute(context);
                    break;
                case Command.SaveVehiclesToFile:
                    new SaveVehicles().Execute(context);
                    break;
                case Command.LoadVehiclesFromFile:
                    new LoadVehicles().Execute(context);
                    break;
                case Command.GetLatetstVehicleEntry:
                    return (T)(object)new GetLatestVehicleEntry().Execute(context);
                case Command.SortVehicles:
                    return (T)(object)new SortVehicles().Execute(context);
                case Command.SearchForVehicles:
                    return (T)(object)new SearchVehicles().Execute(context);
                default:
                    break;
            }
            return default;
        }
    }
}
