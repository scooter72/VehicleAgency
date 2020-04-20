using System;

namespace VehicleAgency.Commands
{
    internal sealed class PrintToScreent : CommandBase<Void>
    {
        public override Void Execute(CommandContext context)
        {
            Console.WriteLine();
            Console.WriteLine($"Printing {context.VehiclesManager.Vehicles.Length} vehicles in repository");
            Console.WriteLine("-------------------------------------------------");
            foreach (var item in context.VehiclesManager.Vehicles)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
            return null;
        }
    }
}
