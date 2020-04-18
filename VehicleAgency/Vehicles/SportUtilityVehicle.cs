using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Vehicles
{
    public class SportUtilityVehicle : Vehicle
    {

        public int NumberOfSeats { get; set; }

        public SportUtilityVehicle(): base(VehicleType.SUV)
        {
        }

        protected override void SetAdditionalInfo(string[] tokens)
        {
            int.TryParse(tokens[tokens.Length - 1], out var numberOfSeats);
            NumberOfSeats = numberOfSeats;
        }

        protected override string GetAdditionalInfo()
        {
            return $"{NumberOfSeats}";
        }
    }
}
