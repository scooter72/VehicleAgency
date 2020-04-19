using System;
using System.IO;
using VehicleAgency.Command;
using VehicleAgency.Vehicles;

namespace VehicleAgency
{
    public class Program
    {
        static void Main(string[] args)
        {
            string dataFilePath = @"c:\temp\vehicles.txt";
            VehiclesManager repository = new VehiclesManager();
            int selection;

            if (!(File.Exists(dataFilePath)))
            {
                File.CreateText(dataFilePath);
            }

            var context = new CommandContext()
            {
                VehiclesManager = repository,
                VehicleInfoInput = () => ReadVehicleInfoFromConsole(),
                LicensePlateInput = () => GetLicensePlateInput(),
                ManufacturerInput = () => GetInput("manufacturer"),
                DataFilePath = dataFilePath,
                SearchCriteriaInput = () => GetSelectedCriteria(typeof(VehiclesSearchCriteria), "Select search criteria:"),
                ProductionYearInput = () => GetNumericInput("production year", 1886, DateTime.Now.Year),
                SortCriteriaInput = () => GetSelectedCriteria(typeof(VehiclesSortCriteria), "Select sort criteria:")
            };

            do
            {
                PrintUsage();

                if (!(int.TryParse(Console.ReadLine(), out selection)) || selection < 0)
                {
                    continue;
                }

                try
                {
                    context.Selection = ((CommandTypes)selection);
                    var result = ProcessUserSelection(context);
                    Console.WriteLine("--");
                    if (result is Vehicle || result is string)
                    {
                        Console.WriteLine(result);
                    }
                    else if (result is Array) 
                    {
                        foreach (var item in (Array)result)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
            while (((CommandTypes)selection) != CommandTypes.Exit);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("--");
            Console.WriteLine("Enter one of the options below:");
            for (int i = 0; i < Enum.GetNames(typeof(CommandTypes)).Length; i++)
            {
                Console.WriteLine(GetUserSelectionOptionsLabels((CommandTypes)i));
            }
        }

        private static String GetUserSelectionOptionsLabels(CommandTypes selection)
        {
            var label = $" {(int)selection} -";
            switch (selection)
            {
                case CommandTypes.InputVehicleData:
                    return $"{label} Input Vehicle Data";
                case CommandTypes.RemoveVehicle:
                    return $"{label} Remove Vehicle";
                case CommandTypes.PrintVehiclesToScreen:
                    return $"{label} Print Vehicles to Screen";
                case CommandTypes.SaveVehiclesToFile:
                    return $"{label} Save Vehicles to File";
                case CommandTypes.LoadVehiclesFromFile:
                    return $"{label} Load Vehicles from File";
                case CommandTypes.GetLatetstVehicleEntry:
                    return $"{label} Get Latest Vehicle Entry";
                case CommandTypes.SortVehicles:
                    return $"{label} Sort Vehicles";
                case CommandTypes.SearchForVehicles:
                    return $"{label} Search Vehicles";
                case CommandTypes.Exit:
                    return $"{label} Exit";
                default:
                    return string.Empty;
            }
        }

        public static object ProcessUserSelection(CommandContext context)
        {
            switch (context.Selection)
            {
                case CommandTypes.InputVehicleData:
                    AddVehicle(context.VehiclesManager, context.VehicleInfoInput);
                    break;
                case CommandTypes.RemoveVehicle:
                    CommandFactory.ExecuteCommand<object>(CommandTypes.RemoveVehicle, context);
                    break;
                case CommandTypes.PrintVehiclesToScreen:
                    CommandFactory.ExecuteCommand<object>(CommandTypes.PrintVehiclesToScreen, context);
                    break;
                case CommandTypes.SaveVehiclesToFile:
                    context.VehiclesManager.SaveVehicles(context.DataFilePath);
                    break;
                case CommandTypes.LoadVehiclesFromFile:
                    context.VehiclesManager.LoadVehicles(context.DataFilePath);
                    break;
                case CommandTypes.GetLatetstVehicleEntry:
                    Console.WriteLine("--");
                    Console.WriteLine("Latest vehicle entry:");
                    return (context.VehiclesManager.GetlatestEntry());
                case CommandTypes.SortVehicles:
                    return (SortVehicles(context));
                case CommandTypes.SearchForVehicles:
                    return (SearchVehicles(context));
                case CommandTypes.Exit:
                    break;
                default:
                    throw new NotSupportedException();
            }
            return null;
        }

        private static Vehicle[] SortVehicles(CommandContext context)
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

        private static Vehicle[] SearchVehicles(CommandContext context)
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

        private static int GetSelectedCriteria(Type options, string label)
        {

            ConsoleMenu.PrintMenu(options, label);
            var selection = ConsoleMenu.GetUserSelection(0, Enum.GetNames(options).Length - 1);
            return selection;
        }

        private static void AddVehicle(VehiclesManager repository, Func<Vehicle> vehicleInputProvider)
        {
            var vehicle = vehicleInputProvider.Invoke();
            if (vehicle != null)
            {
                repository.AddVehicle(vehicle);
            }
        }

        private static string GetLicensePlateInput()
        {
            Console.WriteLine("Enter license plate:");
            return Console.ReadLine();
        }

        private static Vehicle ReadVehicleInfoFromConsole()
        {
            int selectedVehicleType = GetSelectedVehicleType();

            if (selectedVehicleType < 0)
            {
                return null;
            }

            VehicleType vehicleType = (VehicleType)selectedVehicleType;
            Console.WriteLine($"Enter info for vehicle of type '{vehicleType}':");
            Console.WriteLine("--");
            Vehicle vehicle = Vehicle.CreateVehicle(vehicleType);

            vehicle.Manufacturer = GetInput("manufacturer");
            vehicle.Model = GetInput("vehicle model");
            vehicle.ProductionYear = GetNumericInput("production year", 1886, DateTime.Now.Year);
            vehicle.LicensePlate = GetInput("license plate");
            
            GetAdditionalVehicleInfo(vehicle);
            
            return vehicle;
        }

        private static int GetSelectedVehicleType()
        {

            ConsoleMenu.PrintMenu(typeof(VehicleType), "Select Vehicle type:");
            var selection = ConsoleMenu.GetUserSelection(0, Enum.GetNames(typeof(VehicleType)).Length - 1);
            return selection;
        }

        private static int GetNumericInput(string inputName, int min, int max)
        {
            Console.WriteLine($"Enter {inputName} ({min}-{max}):"); 
            
            int value = Util.GetNumericInput(min, max);

            if (value < 0)
            {
                throw new ArgumentException($"Value of '{inputName}' is not not valid");
            }

            return value;
        }

        #region Additional Vehicle Info
        private static void GetAdditionalVehicleInfo(Vehicle vehicle)
        {
            switch (vehicle.VehicleType)
            {
                case VehicleType.Truck: GetAdditionalVehicleInfo((Truck)vehicle); break;
                case VehicleType.Trailer: GetAdditionalVehicleInfo((Trailer)vehicle); break; 
                case VehicleType.Motorcycle: GetAdditionalVehicleInfo((Motorcycle)vehicle); break;
                case VehicleType.SUV: GetAdditionalVehicleInfo((SportUtilityVehicle)vehicle); break;
            }
        }
        
        private static void GetAdditionalVehicleInfo(Motorcycle motorcycle)
        {
            motorcycle.VehicleCategory = GetInput("Vehicle Category");
        }

        private static void GetAdditionalVehicleInfo(Truck truck)
        {
            Console.WriteLine("Enter overall weight: (4000-100000)");
            truck.OverallWeight = GetNumericInput("OverallWeight", 4000, 100000);
        }

        private static void GetAdditionalVehicleInfo(Trailer trailer)
        {
            Console.WriteLine("Enter load capcity: (4000-100000)");
            trailer.VehicleLoadCapcity = GetNumericInput("VehicleLoadCapcity", 4000, 100000);
        }

        private static void GetAdditionalVehicleInfo(SportUtilityVehicle suv)
        {
            Console.WriteLine("Enter number of seats (5-9): ");
            suv.NumberOfSeats = Util.GetNumericInput(5, 9);
        }
        #endregion  

        private static string GetInput(string inputName)
        {
            Console.WriteLine($"Enter {inputName}: ");

            string input;

            for (int i = 0; i < 3; i++)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Value of '{inputName}' cannot be empty");
                }
                else
                {
                    return input;
                }
            }

            throw new ArgumentException($"Value of '{inputName}' cannot be empty");
        }

    }
}
