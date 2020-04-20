namespace VehicleAgency.Commands
{
    internal sealed class AddVehicle : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            var vehicle = context.VehicleInfoInput.Invoke();
            if (vehicle != null)
            {
                context.VehiclesManager.AddVehicle(vehicle);
            }

            return null;
        }
    }
}
