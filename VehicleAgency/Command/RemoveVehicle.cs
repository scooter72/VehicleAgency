using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Command
{
    internal sealed class RemoveVehicle : CommandBase<object>
    {
        public override object Execute(CommandContext context)
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
