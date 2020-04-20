using System;
using VehicleAgency.Vehicles;

namespace VehicleAgency.Commands
{
    internal sealed class SortVehicles : CommandBase<Vehicle[]>
    {
        public override Vehicle[] Execute(CommandContext context)
        {
            var selection = context.SortCriteriaInput.Invoke();
            if (selection > -1)
            {
                Vehicle[] vehicles;
                switch ((VehiclesSortCriteria)selection)
                {
                    case VehiclesSortCriteria.Manufacturer:
                        vehicles = context.VehiclesManager.SortByManufacturer();
                        break;
                    case VehiclesSortCriteria.ProductionYear:
                        vehicles = context.VehiclesManager.SortByProductionYear();
                        break;
                    case VehiclesSortCriteria.ManufacturerAndProductionYear:
                        vehicles = context.VehiclesManager.SortByManufacturerAndProductionYear();
                        break;
                    default:
                        throw new NotSupportedException();
                }
                Console.WriteLine("--");
                Console.WriteLine($"{vehicles.Length} Vehicles sorted by {(VehiclesSortCriteria)selection}");
                return vehicles;
            }
            return null;
        }
    }
}
