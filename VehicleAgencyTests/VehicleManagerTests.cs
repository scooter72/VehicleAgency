using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VehicleAgency.Vehicles;

namespace VehicleAgency.Tests
{
    [TestClass()]
    public class VehicleManagerTests
    {
        [TestMethod()]
        public void AddVehicleTest()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car 
            { 
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            repository.AddVehicle(expectedVehicle);
            CollectionAssert.Contains(repository.Vehicles, expectedVehicle);
        }

        [TestMethod()]

        public void RemoveVehicleTest()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            repository.AddVehicle(expectedVehicle);
            CollectionAssert.Contains(repository.Vehicles, expectedVehicle);
            repository.RemoveVehicle(expectedVehicle.LicensePlate);
            CollectionAssert.DoesNotContain(repository.Vehicles, expectedVehicle);
        }

        [TestMethod()]
        public void SaveLoadTest()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };


            repository.AddVehicle(expectedVehicle);
            var tempFile = Path.GetTempFileName();
            repository.SaveVehicles(tempFile);
            VehiclesManager repository2 = new VehiclesManager();
            repository2.LoadVehicles(tempFile);

            Vehicle actual = null;

            foreach (var vehicle in repository2.Vehicles)
            {
                if (vehicle.CompareTo(expectedVehicle) == 0) 
                {
                    actual = vehicle;
                    break;
                }
            }

            Assert.IsNotNull(actual, "Vehicle not found");
        }

        [TestMethod()]
        public void FindVehicleByLicecnsePlateTest()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };


            repository.AddVehicle(expectedVehicle);

            Assert.IsNotNull(repository.FindVehicleByLicecnsePlate(expectedVehicle.LicensePlate), 
                "Vehicle not found");
        }

        [TestMethod()]
        public void FindVehiclesByManufacturer()
        {
            VehiclesManager repository = new VehiclesManager();
            var vehicle1 = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            var vehicle2 = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2001,
                LicensePlate = "NY2001",
            };

            repository.AddVehicles(new Vehicle[] { vehicle1, vehicle2});

            Assert.AreEqual(2, repository.FindVehiclesByManufacturer("Ford").Length);
        }

        [TestMethod()]
        public void FindVehiclesByProductionYear()
        {
            VehiclesManager repository = new VehiclesManager();
            var vehicle1 = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            var vehicle2 = new Car
            {
                Manufacturer = "Toyota",
                Model = "Corolla",
                ProductionYear = 2000,
                LicensePlate = "NY2001",
            };

            repository.AddVehicles(new Vehicle[] { vehicle1, vehicle2 });

            Assert.AreEqual(2, repository.FindVehiclesByProductionYear(2000).Length);
        }

        [TestMethod()]

        public void SortByManufacturerTest()
        {
            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Manufacturer = "B",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Manufacturer = "A",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000A",
                } 
            };

            VehiclesManager repository = new VehiclesManager();
            repository.AddVehicles(vehicles);
            var vehicles2 = repository.Vehicles;
            Assert.IsTrue(vehicles2[0].Manufacturer.Equals("C"));
            Assert.IsTrue(vehicles2[1].Manufacturer.Equals("B"));
            Assert.IsTrue(vehicles2[2].Manufacturer.Equals("A"));
            var sorted = repository.SortByManufacturer();
            Assert.IsTrue(sorted[0].Manufacturer.Equals("A"));
            Assert.IsTrue(sorted[1].Manufacturer.Equals("B"));
            Assert.IsTrue(sorted[2].Manufacturer.Equals("C"));

        }

        [TestMethod()]
        public void SortByProductionYearTest()
        {
            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "Ford",
                    Model = "Mustang",
                    ProductionYear = 2003,
                    LicensePlate = "NY2003",
                },
                new Car
                {
                    Manufacturer = "Ford",
                    Model = "Corolla",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002",
                },

                new Car
                {
                    Manufacturer = "Ford",
                    Model = "Mustang",
                    ProductionYear = 2001,
                    LicensePlate = "NY2001",
                }
            };

            VehiclesManager repository = new VehiclesManager();
            repository.AddVehicles(vehicles);
            var vehicles2 = repository.Vehicles;
            Assert.IsTrue(vehicles2[0].ProductionYear == 2003);
            Assert.IsTrue(vehicles2[1].ProductionYear == 2002);
            Assert.IsTrue(vehicles2[2].ProductionYear == 2001);
            var sorted = repository.SortByProductionYear();
            Assert.IsTrue(sorted[0].ProductionYear == 2001);
            Assert.IsTrue(sorted[1].ProductionYear == 2002);
            Assert.IsTrue(sorted[2].ProductionYear == 2003);
        }

        [TestMethod()]
        public void SortByManufacturerAndProductionYear()
        {
            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "D",
                    Model = "Corolla",
                    ProductionYear = 2010,
                    LicensePlate = "NY2010D",
                },
                new Car
                {
                    Manufacturer = "D",
                    Model = "Corolla",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002D",
                },
                new Car
                {
                    Manufacturer = "D",
                    Model = "Corolla",
                    ProductionYear = 2001,
                    LicensePlate = "NY2001D",
                },
                new Car
                {
                    Manufacturer = "A",
                    Model = "Mustang",
                    ProductionYear = 2005,
                    LicensePlate = "NY2005A",
                },
                new Car
                {
                    Manufacturer = "A",
                    Model = "Mustang",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002A",
                },
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2005,
                    LicensePlate = "NY2010C",
                },
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2010,
                    LicensePlate = "NY2010C",
                }
            };

            string[] lines = vehicles.Select(v => v.ToString()).ToArray();
            File.WriteAllLines(@"c:\temp\vehicles.txt", lines );

            VehiclesManager repository = new VehiclesManager();
            repository.AddVehicles(vehicles);
            var vehicles2 = repository.Vehicles;
            var sorted = repository.SortByManufacturerAndProductionYear();
            Assert.IsTrue(sorted[0].Manufacturer.Equals("A") && sorted[1].Manufacturer.Equals("A") && sorted[0].ProductionYear < sorted[1].ProductionYear);
            Assert.IsTrue(sorted[2].Manufacturer.Equals("C") && sorted[3].Manufacturer.Equals("C") && sorted[3].Manufacturer.Equals("C") 
                && sorted[2].ProductionYear < sorted[3].ProductionYear && sorted[3].ProductionYear < sorted[4].ProductionYear);
            Assert.IsTrue(sorted[5].Manufacturer.Equals("D") && sorted[6].Manufacturer.Equals("D") && sorted[7].Manufacturer.Equals("D")
                && sorted[5].ProductionYear < sorted[6].ProductionYear && sorted[6].ProductionYear < sorted[7].ProductionYear);
        }


        [TestMethod]
        public void GetlatestEntryTest()
        {
            VehiclesManager repository = new VehiclesManager();

            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Manufacturer = "B",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Manufacturer = "A",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000A",
                }
            };
            repository.AddVehicles(vehicles);
            Assert.AreEqual(vehicles[2], repository.GetlatestEntry());
        }

       

    }
}