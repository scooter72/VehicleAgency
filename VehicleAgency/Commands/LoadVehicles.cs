namespace VehicleAgency.Commands
{
    internal sealed class LoadVehicles : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            context.VehiclesManager.LoadVehicles(context.DataFilePath);
            return null;
        }
    }
}
