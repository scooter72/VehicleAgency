using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Make = "Ford",
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
                Make = "Ford",
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
                Make = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };


            repository.AddVehicle(expectedVehicle);
            var tempFile = Path.GetTempFileName();
            repository.SaveVehicles(tempFile);
            VehiclesManager repository2 = new VehiclesManager();
            repository2.LoadVehicles(tempFile);

            Assert.IsNotNull(repository2.Vehicles.FirstOrDefault(i => i.CompareTo(expectedVehicle) == 0), "Vehicle not found");
        }

        [TestMethod()]
        public void FindVehicleByLicecnsePlateTest()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car
            {
                Make = "Ford",
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
                Make = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            var vehicle2 = new Car
            {
                Make = "Ford",
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
                Make = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };

            var vehicle2 = new Car
            {
                Make = "Toyota",
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
                    Make = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Make = "B",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Make = "A",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000A",
                } 
            };

            VehiclesManager repository = new VehiclesManager();
            repository.AddVehicles(vehicles);
            var vehicles2 = repository.Vehicles;
            Assert.IsTrue(vehicles2[0].Make.Equals("C"));
            Assert.IsTrue(vehicles2[1].Make.Equals("B"));
            Assert.IsTrue(vehicles2[2].Make.Equals("A"));
            var sorted = repository.SortByManufacturer();
            Assert.IsTrue(sorted[0].Make.Equals("A"));
            Assert.IsTrue(sorted[1].Make.Equals("B"));
            Assert.IsTrue(sorted[2].Make.Equals("C"));

        }

        [TestMethod()]
        public void SortByProductionYearTest()
        {
            var vehicles = new Vehicle[] {
                new Car
                {
                    Make = "Ford",
                    Model = "Mustang",
                    ProductionYear = 2003,
                    LicensePlate = "NY2003",
                },
                new Car
                {
                    Make = "Ford",
                    Model = "Corolla",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002",
                },

                new Car
                {
                    Make = "Ford",
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
                    Make = "D",
                    Model = "Corolla",
                    ProductionYear = 2010,
                    LicensePlate = "NY2010D",
                },
                new Car
                {
                    Make = "D",
                    Model = "Corolla",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002D",
                },
                new Car
                {
                    Make = "D",
                    Model = "Corolla",
                    ProductionYear = 2001,
                    LicensePlate = "NY2001D",
                },
                new Car
                {
                    Make = "A",
                    Model = "Mustang",
                    ProductionYear = 2005,
                    LicensePlate = "NY2005A",
                },
                new Car
                {
                    Make = "A",
                    Model = "Mustang",
                    ProductionYear = 2002,
                    LicensePlate = "NY2002A",
                },
                new Car
                {
                    Make = "C",
                    Model = "Mustang",
                    ProductionYear = 2005,
                    LicensePlate = "NY2010C",
                },
                new Car
                {
                    Make = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Make = "C",
                    Model = "Mustang",
                    ProductionYear = 2010,
                    LicensePlate = "NY2010C",
                }
            };

            VehiclesManager repository = new VehiclesManager();
            repository.AddVehicles(vehicles);
            var vehicles2 = repository.Vehicles;
            var sorted = repository.SortByManufacturerAndProductionYear();
            Assert.IsTrue(sorted[0].Make.Equals("A") && sorted[1].Make.Equals("A") && sorted[0].ProductionYear < sorted[1].ProductionYear);
            Assert.IsTrue(sorted[2].Make.Equals("C") && sorted[3].Make.Equals("C") && sorted[3].Make.Equals("C") 
                && sorted[2].ProductionYear < sorted[3].ProductionYear && sorted[3].ProductionYear < sorted[4].ProductionYear);
            Assert.IsTrue(sorted[5].Make.Equals("D") && sorted[6].Make.Equals("D") && sorted[7].Make.Equals("D")
                && sorted[5].ProductionYear < sorted[6].ProductionYear && sorted[6].ProductionYear < sorted[7].ProductionYear);
        }


        [TestMethod]
        public void GetlatestEntryTest()
        {
            VehiclesManager repository = new VehiclesManager();

            var vehicles = new Vehicle[] {
                new Car
                {
                    Make = "C",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Make = "B",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Make = "A",
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