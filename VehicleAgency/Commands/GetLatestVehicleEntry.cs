using System;

namespace VehicleAgency.Commands
{
    class GetLatestVehicleEntry : CommandBase<Vehicle>
    {
        public override Vehicle Execute(CommandContext context)
        {
            Console.WriteLine("--");
            Console.WriteLine("Latest vehicle entry:");
            return (context.VehiclesManager.GetlatestEntry());
        }
    }
}
