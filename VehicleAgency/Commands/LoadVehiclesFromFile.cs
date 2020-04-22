namespace VehicleAgency.Commands
{
    internal sealed class LoadVehiclesFromFile : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            context.VehiclesManager.LoadVehicles(context.DataFilePath);
            return null;
        }
    }
}
