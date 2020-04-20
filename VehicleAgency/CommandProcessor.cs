using System;
using System.IO;
using VehicleAgency.Commands;
using VehicleAgency.Vehicles;

namespace VehicleAgency
{
    public sealed class CommandProcesseor
    {
        public void ProcessCommands()
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
                    context.Command = ((Command)selection);
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
            while (((Command)selection) != Command.Exit);
        }

        private void PrintUsage()
        {
            Console.WriteLine("--");
            Console.WriteLine("Enter one of the options below:");
            for (int i = 0; i < Enum.GetNames(typeof(Command)).Length; i++)
            {
                Console.WriteLine(GetUserSelectionOptionsLabels((Command)i));
            }
        }

        private String GetUserSelectionOptionsLabels(Command selection)
        {
            var label = $" {(int)selection} -";
            switch (selection)
            {
                case Command.AddVehicle:
                    return $"{label} Input Vehicle Data";
                case Command.RemoveVehicle:
                    return $"{label} Remove Vehicle";
                case Command.PrintVehiclesToScreen:
                    return $"{label} Print Vehicles to Screen";
                case Command.SaveVehiclesToFile:
                    return $"{label} Save Vehicles to File";
                case Command.LoadVehiclesFromFile:
                    return $"{label} Load Vehicles from File";
                case Command.GetLatetstVehicleEntry:
                    return $"{label} Get Latest Vehicle Entry";
                case Command.SortVehicles:
                    return $"{label} Sort Vehicles";
                case Command.SearchForVehicles:
                    return $"{label} Search Vehicles";
                case Command.Exit:
                    return $"{label} Exit";
                default:
                    return string.Empty;
            }
        }

        public object ProcessUserSelection(CommandContext context)
        {
            return CommandFactory.ExecuteCommand<object>(context.Command, context);
        }

        private int GetSelectedCriteria(Type options, string label)
        {

            ConsoleMenu.PrintMenu(options, label);
            var selection = ConsoleMenu.GetUserSelection(0, Enum.GetNames(options).Length - 1);
            return selection;
        }

        private string GetLicensePlateInput()
        {
            Console.WriteLine("Enter license plate:");
            return Console.ReadLine();
        }

        private Vehicle ReadVehicleInfoFromConsole()
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
            vehicle.MaxSpeed = GetNumericInput("max speed", 0, 320);
            
            GetAdditionalVehicleInfo(vehicle);
            
            return vehicle;
        }

        private int GetSelectedVehicleType()
        {

            ConsoleMenu.PrintMenu(typeof(VehicleType), "Select Vehicle type:");
            var selection = ConsoleMenu.GetUserSelection(0, Enum.GetNames(typeof(VehicleType)).Length - 1);
            return selection;
        }

        private int GetNumericInput(string inputName, int min, int max)
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
        private void GetAdditionalVehicleInfo(Vehicle vehicle)
        {
            switch (vehicle.VehicleType)
            {
                case VehicleType.Truck: GetAdditionalVehicleInfo((Truck)vehicle); break;
                case VehicleType.Trailer: GetAdditionalVehicleInfo((Trailer)vehicle); break; 
                case VehicleType.Motorcycle: GetAdditionalVehicleInfo((Motorcycle)vehicle); break;
                case VehicleType.SUV: GetAdditionalVehicleInfo((SportUtilityVehicle)vehicle); break;
            }
        }
        
        private void GetAdditionalVehicleInfo(Motorcycle motorcycle)
        {
            motorcycle.VehicleCategory = GetInput("Vehicle Category");
        }

        private void GetAdditionalVehicleInfo(Truck truck)
        {
            Console.WriteLine("Enter overall weight: (4000-100000)");
            truck.TotalWeight = GetNumericInput("OverallWeight", 4000, 100000);
        }

        private void GetAdditionalVehicleInfo(Trailer trailer)
        {
            Console.WriteLine("Enter load capcity: (4000-100000)");
            trailer.VehicleLoadCapcity = GetNumericInput("VehicleLoadCapcity", 4000, 100000);
        }

        private void GetAdditionalVehicleInfo(SportUtilityVehicle suv)
        {
            Console.WriteLine("Enter number of seats (5-9): ");
            suv.NumberOfSeats = Util.GetNumericInput(5, 9);
        }
        #endregion  

        private string GetInput(string inputName)
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
