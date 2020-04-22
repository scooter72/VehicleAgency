using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAgency.Vehicles;

namespace VehicleAgency
{
    public abstract class Vehicle : IComparable<Vehicle>
    {

        public int ProductionYear { get; set; }

        public int MaxSpeed { get; set; }
        
        public string Model { get; set; }

        public string Make { get; set; }

        public string LicensePlate { get; set; }
        public VehicleType VehicleType { get; set; }

        protected Vehicle(VehicleType vehicleType)
        {
            VehicleType = vehicleType;
        }

        public static Vehicle Parse(string data) 
        {
            string[] tokens = data.Split(',');

            if (!(Enum.TryParse(tokens[0], out VehicleType vehicleType)))
            {
                throw new Exception($"Unknown vehicle type: '{tokens[0]}'");
            }

            string manufacturer = tokens[1];
            string model = tokens[2];
            int productionYear = int.Parse(tokens[3]);
            string licensePlate = tokens[4];

            Vehicle vehicle = CreateVehicle(vehicleType);

            vehicle.Make = manufacturer;
            vehicle.Model = model;
            vehicle.ProductionYear = productionYear;
            vehicle.LicensePlate = licensePlate;
            vehicle.SetAdditionalInfo(tokens);

            return vehicle;
        }

        public static Vehicle CreateVehicle(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Car:        return new Car();
                case VehicleType.Truck:      return new Truck();
                case VehicleType.Trailer:    return new Trailer(); 
                case VehicleType.Motorcycle: return new Motorcycle();
                case VehicleType.SUV:        return new SportUtilityVehicle();
            }

            return null;
        }

        public override string ToString()
        {
            return $"{VehicleType},{Make},{Model},{ProductionYear},{LicensePlate},{GetAdditionalInfo()}" ;
        }

        protected virtual string GetAdditionalInfo()
        {
            return string.Empty;
        }

        protected virtual void SetAdditionalInfo(string[] tokens)
        {
        }

        public int CompareTo(Vehicle other)
        {
            return (VehicleType == other.VehicleType
                && Make.CompareTo(other.Make) == 0
                && Model.CompareTo(other.Model) == 0
                && ProductionYear == other.ProductionYear
                && LicensePlate.CompareTo(other.LicensePlate) == 0) ?  0 : -1; 
        }
    }
}
