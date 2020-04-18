using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Vehicles
{
    public class Trailer : Vehicle
    {
        public int VehicleLoadCapcity { get; set;}

        public Trailer() : base(VehicleType.Trailer)
        {
        }

        protected override void SetAdditionalInfo(string[] tokens)
        {
            int.TryParse(tokens[tokens.Length - 1], out var vehicleLoadCapcity);
            VehicleLoadCapcity = vehicleLoadCapcity;
        }

        protected override string GetAdditionalInfo()
        {
            return $"{VehicleLoadCapcity}";
        }
    }
}
