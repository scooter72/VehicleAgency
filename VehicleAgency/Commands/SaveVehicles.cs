namespace VehicleAgency.Commands
{
    internal sealed class SaveVehicles : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            context.VehiclesManager.SaveVehicles(context.DataFilePath);
            return null;
        }
    }
}
