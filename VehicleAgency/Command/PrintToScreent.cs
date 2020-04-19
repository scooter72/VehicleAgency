using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Command
{
    internal sealed class PrintToScreent : CommandBase<object>
    {
        public override object Execute(CommandContext context)
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
