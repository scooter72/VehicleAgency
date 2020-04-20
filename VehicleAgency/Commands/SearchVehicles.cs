using System;
using VehicleAgency.Vehicles;

namespace VehicleAgency.Commands
{
    internal sealed class SearchVehicles : CommandBase<Vehicle[]>
    {
        public override Vehicle[] Execute(CommandContext context)
        {
            var selection = context.SearchCriteriaInput.Invoke();

            if (selection > -1)
            {
                Vehicle[] vehicles = null;
                string criteria = null;

                switch ((VehiclesSearchCriteria)selection)
                {
                    case VehiclesSearchCriteria.LicensePlate:
                        criteria = context.LicensePlateInput.Invoke();
                        var vehicle = context.VehiclesManager.FindVehicleByLicecnsePlate(criteria);
                        if (vehicle != null)
                        {
                            vehicles = new Vehicle[] { vehicle };
                        }
                        break;
                    case VehiclesSearchCriteria.Manufacturer:
                        criteria = context.ManufacturerInput.Invoke();
                        vehicles = (context.VehiclesManager.FindVehiclesByManufacturer(criteria));
                        break;
                    case VehiclesSearchCriteria.ProductionYear:
                        var year = context.ProductionYearInput.Invoke();
                        vehicles = (context.VehiclesManager.FindVehiclesByProductionYear(year));
                        criteria = year.ToString();
                        break;
                    default:
                        throw new NotSupportedException();
                }
                Console.WriteLine("--");
                Console.WriteLine($"{vehicles.Length} Vehicles found by {(VehiclesSearchCriteria)selection} -> '{criteria}'");
                return vehicles;
            }
            return null;
        }
    }
}
