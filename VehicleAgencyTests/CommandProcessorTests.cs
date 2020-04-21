using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using VehicleAgency.Vehicles;

namespace VehicleAgency.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void TestInputVehicle()
        {
            VehiclesManager repository = new VehiclesManager();
            var expectedVehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                VehicleInfoInput = () => expectedVehicle,
                Command = Command.AddVehicle
            };
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            commandProcesseor.ProcessUserSelection(context);
            CollectionAssert.Contains(repository.Vehicles, expectedVehicle);
        }

        [TestMethod()]
        public void TestRemoveVehicle()
        {
            VehiclesManager repository = new VehiclesManager();
            var vehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };
            repository.AddVehicle(vehicle);
            CollectionAssert.Contains(repository.Vehicles, vehicle);
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                LicensePlateInput = () => vehicle.LicensePlate,
                Command = Command.RemoveVehicle
            };
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            commandProcesseor.ProcessUserSelection(context);
            CollectionAssert.DoesNotContain(repository.Vehicles, vehicle);
        }



        [TestMethod()]
        public void TestFindByLicensePlate()
        {
            VehiclesManager repository = new VehiclesManager();
            var vehicle = new Car
            {
                Manufacturer = "Ford",
                Model = "Mustang",
                ProductionYear = 2000,
                LicensePlate = "NY2000",
            };
            repository.AddVehicle(vehicle);
            CollectionAssert.Contains(repository.Vehicles, vehicle);
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                LicensePlateInput = () => vehicle.LicensePlate,
                Command = Command.SearchForVehicles,
                SearchCriteriaInput = () => (int)VehiclesSearchCriteria.LicensePlate
            };
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            var result = commandProcesseor.ProcessUserSelection(context);
            Assert.IsTrue(result is Array, "an array was expected as a result");
            Assert.IsTrue(((Array)result).Length == 1, "an array with one item was expected");
            Assert.AreEqual(vehicle, ((Array)result).GetValue(0));
        }

        [TestMethod()]
        public void TestGetLatestEntry()
        {
            VehiclesManager repository = new VehiclesManager();
            var vehicle = new Car
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
                ProductionYear = 2000,
                LicensePlate = "NY2002",
            };
            repository.AddVehicle(vehicle);
            repository.AddVehicle(vehicle2);
            CollectionAssert.Contains(repository.Vehicles, vehicle);
            CollectionAssert.Contains(repository.Vehicles, vehicle2);
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                LicensePlateInput = () => vehicle.LicensePlate,
                Command = Command.GetLatetstVehicleEntry
            };
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            var actual = commandProcesseor.ProcessUserSelection(context);
            Assert.AreEqual(vehicle2, actual);
        }

        [TestMethod()]
        public void TestSaveLoad()
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
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                DataFilePath = tempFile,
                Command = Command.SaveVehiclesToFile
            };
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            commandProcesseor.ProcessUserSelection(context);

            VehiclesManager repository2 = new VehiclesManager();
            context.VehiclesManager = repository2;
            context.Command = Command.LoadVehiclesFromFile;
            commandProcesseor.ProcessUserSelection(context);
            Assert.IsNotNull(repository2.Vehicles.FirstOrDefault(i=> i.CompareTo(expectedVehicle) == 0), "Vehicle not found");
        }


        [TestMethod()]
        public void TestLatestEntry() 
        {
            VehiclesManager repository = new VehiclesManager();
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                Command = Command.GetLatetstVehicleEntry
            };

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
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            object actual = commandProcesseor.ProcessUserSelection(context);

            Assert.AreEqual(vehicles[2], actual);
        }


        [TestMethod()]
        public void TestSortVehicleByManufacturer()
        {
            VehiclesManager repository = new VehiclesManager();
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                Command = Command.SortVehicles,
                SortCriteriaInput = () => (int)VehiclesSortCriteria.Manufacturer
            };

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
                    Manufacturer = "A",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Manufacturer = "B",
                    Model = "Mustang",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000A",
                }
            };
            repository.AddVehicles(vehicles);
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            object result = commandProcesseor.ProcessUserSelection(context);

            Assert.IsTrue(result is Array, "an array was expected as a result");
            Assert.IsTrue(((Array)result).Length == 3, "an array with three items was expected");
            Assert.AreEqual(vehicles[1], ((Array)result).GetValue(0));
            Assert.AreEqual(vehicles[2], ((Array)result).GetValue(1));
            Assert.AreEqual(vehicles[0], ((Array)result).GetValue(2));
        }

        [TestMethod()]

        public void TestSortVehicleByProductionYear()
        {
            VehiclesManager repository = new VehiclesManager();
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                Command = Command.SortVehicles,
                SortCriteriaInput = () => (int)VehiclesSortCriteria.ProductionYear
            };

            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2007,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Manufacturer = "A",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Manufacturer = "B",
                    Model = "Mustang",
                    ProductionYear = 2003,
                    LicensePlate = "NY2000A",
                }
            };
            repository.AddVehicles(vehicles);
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            object result = commandProcesseor.ProcessUserSelection(context);

            Assert.IsTrue(result is Array, "an array was expected as a result");
            Assert.IsTrue(((Array)result).Length == 3, "an array with three items was expected");
            Assert.AreEqual(vehicles[1], ((Array)result).GetValue(0));
            Assert.AreEqual(vehicles[2], ((Array)result).GetValue(1));
            Assert.AreEqual(vehicles[0], ((Array)result).GetValue(2));
        }

        [TestMethod()]
        public void TestSortVehicleByMAnufacturerAndProductionYear()
        {
            VehiclesManager repository = new VehiclesManager();
            var context = new CommandContext()
            {
                VehiclesManager = repository,
                Command = Command.SortVehicles,
                SortCriteriaInput = () => (int)VehiclesSortCriteria.ManufacturerAndProductionYear
            };

            var vehicles = new Vehicle[] {
                new Car
                {
                    Manufacturer = "C",
                    Model = "Mustang",
                    ProductionYear = 2007,
                    LicensePlate = "NY2000C",
                },
                new Car
                {
                    Manufacturer = "C",
                    Model = "Corolla",
                    ProductionYear = 2000,
                    LicensePlate = "NY2000B",
                },

                new Car
                {
                    Manufacturer = "B",
                    Model = "Mustang",
                    ProductionYear = 2003,
                    LicensePlate = "NY2000A",
                },
                new Car
                {
                    Manufacturer = "B",
                    Model = "Mustang",
                    ProductionYear = 2006,
                    LicensePlate = "NY2000A",
                }
            };
            repository.AddVehicles(vehicles);
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            object result = commandProcesseor.ProcessUserSelection(context);

            Assert.IsTrue(result is Array, "an array was expected as a result");
            Assert.IsTrue(((Array)result).Length == 4, "an array with four items was expected");
            Assert.AreEqual(vehicles[2], ((Array)result).GetValue(0));
            Assert.AreEqual(vehicles[3], ((Array)result).GetValue(1));
            Assert.AreEqual(vehicles[1], ((Array)result).GetValue(2));
            Assert.AreEqual(vehicles[0], ((Array)result).GetValue(3));
        }

    }
}