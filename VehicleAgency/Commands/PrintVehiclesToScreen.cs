using System;

namespace VehicleAgency.Commands
{
    internal sealed class PrintVehiclesToScreen : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            Console.WriteLine();
            Console.WriteLine($"Printing {context.VehiclesManager.Vehicles.Length} vehicles in repository");
            Console.WriteLine("-------------------------------------------------");
            Array.ForEach(context.VehiclesManager.Vehicles, i => Console.WriteLine(i));
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
            return null;
        }
    }
}
