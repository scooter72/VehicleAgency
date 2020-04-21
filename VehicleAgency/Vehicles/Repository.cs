using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VehicleAgency.Vehicles
{
    internal class Repository
    {
        private readonly List<Vehicle> vehicles = new List<Vehicle>();

        internal Vehicle[] Vehicles { get => vehicles.ToArray(); }

        internal void Load(string path)
        {
            Array.ForEach(File.ReadAllLines(path), line => vehicles.Add(Vehicle.Parse(line)));
            Console.WriteLine($"{vehicles.Count} vehicle loaded from file {path}");
        }

        internal void Save(string path)
        {
            List<string> lines = new List<string>(vehicles.Count);
            vehicles.ForEach(vehicle => lines.Add(vehicle.ToString()));
            File.WriteAllLines(path, lines);
            Console.WriteLine($"{vehicles.Count} vehicle saved to file {path}");
        }

        internal void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            Console.WriteLine($"Vehicle {vehicle} added");
        }

        internal void AddVehicles(Vehicle[] vehicles)
        {
            Array.ForEach(vehicles, i => AddVehicle(i));
        }

        internal Vehicle[] FindVehiclesByProductionYear(int year)
        {
            return vehicles.Where(i => i.ProductionYear == year).ToArray();
        }

        internal Vehicle FindVehicleByLicecnsePlate(string licensePlate)
        {
            return vehicles.FirstOrDefault(i => i.LicensePlate == licensePlate);
        }

        internal Vehicle[] FindVehiclesByManufacturer(string manufacturer)
        {
            return vehicles.Where(i => i.Manufacturer == manufacturer).ToArray();
        }


        internal Vehicle GetlatestEntry()
        {
            return vehicles.Count > 0 ? vehicles[vehicles.Count - 1] : null;
        }

        internal void RemoveVehicle(string licensePlate)
        {
            vehicles.RemoveAll(i => i.LicensePlate == licensePlate);
        }


        internal Vehicle[] SortByManufacturer()
        {
            List<Vehicle> copy = new List<Vehicle>(vehicles);
            copy.Sort((x, y) => x.Manufacturer.CompareTo(y.Manufacturer));
            return copy.ToArray();
        }

        internal Vehicle[] SortByProductionYear()
        {
            List<Vehicle> copy = new List<Vehicle>(vehicles);
            copy.Sort((x, y) => x.ProductionYear.CompareTo(y.ProductionYear));
            return copy.ToArray();
        }

        internal Vehicle[] SortByManufacturerAndProductionYear()
        {
            List<Vehicle> copy = new List<Vehicle>(vehicles);
            copy.Sort((a, b) =>
            {
                var firstCompare = a.Manufacturer.CompareTo(b.Manufacturer);
                return firstCompare != 0 ? firstCompare : a.ProductionYear.CompareTo(b.ProductionYear);
            });

            return copy.ToArray();
        }
    }
}
