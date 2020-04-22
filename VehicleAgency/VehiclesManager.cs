using VehicleAgency.Vehicles;

namespace VehicleAgency
{
    public class VehiclesManager
    {
        private readonly VehicleInventory repository = new VehicleInventory();

        public Vehicle[] Vehicles { get => repository.Vehicles;}

        public void AddVehicle(Vehicle vehicle) 
        {
            repository.AddVehicle(vehicle);
        }


        public void AddVehicles(Vehicle[] vehicles)
        {
            repository.AddVehicles(vehicles);
        }

        public void RemoveVehicle(string licensePlate) 
        {
            repository.RemoveVehicle(licensePlate);
        }
        
        public void LoadVehicles(string path) 
        {
            repository.Load(path);
        }

        public void SaveVehicles(string path) 
        {
            repository.Save(path);
        }

        public Vehicle FindVehicleByLicecnsePlate(string licensePlate)
        {
            return repository.FindVehicleByLicecnsePlate(licensePlate);
        }

        public Vehicle[] SortByManufacturerAndProductionYear()
        {
            return repository.SortByManufacturerAndProductionYear();
        }


        public Vehicle[] SortByManufacturer()
        {
            return repository.SortByManufacturer();
        }

        public Vehicle[] SortByProductionYear()
        {
            return repository.SortByProductionYear();
        }

        public Vehicle[] FindVehiclesByManufacturer(string manufacturer) 
        {
            return repository.FindVehiclesByManufacturer(manufacturer);
        }


        public Vehicle GetlatestEntry()
        {
            return repository.GetlatestEntry();
        }

        public Vehicle[] FindVehiclesByProductionYear(int year)
        {
            return repository.FindVehiclesByProductionYear(year);
        }
    }
}
