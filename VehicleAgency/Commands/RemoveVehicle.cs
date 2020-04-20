using System;

namespace VehicleAgency.Commands
{
    internal sealed class RemoveVehicle : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            var licensePlate = context.LicensePlateInput.Invoke();
            var vehicleToRemove = context.VehiclesManager.FindVehicleByLicecnsePlate(licensePlate);
            
            if (vehicleToRemove != null)
            {
                context.VehiclesManager.RemoveVehicle(licensePlate);
                Console.WriteLine($"Vehicle with license plate: '{licensePlate}' removed from repository");
            }
            else
            {
                Console.WriteLine($"Vehicle with licewnse plate: '{licensePlate}' not found");
            }

            return null;
        }
    }
}
