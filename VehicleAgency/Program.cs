using System;
using System.IO;
using VehicleAgency.Commands;
using VehicleAgency.Vehicles;

namespace VehicleAgency
{
    public class Program
    {
        static void Main(string[] args)
        {
            CommandProcesseor commandProcesseor = new CommandProcesseor();
            commandProcesseor.ProcessCommands();
        }
    }
}
