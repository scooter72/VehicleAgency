namespace VehicleAgency.Commands
{
    internal sealed class SaveVehiclesToFile : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            context.VehiclesManager.SaveVehicles(context.DataFilePath);
            return null;
        }
    }
}
