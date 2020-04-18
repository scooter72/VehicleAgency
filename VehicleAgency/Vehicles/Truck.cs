using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Vehicles
{
    public class Truck : Vehicle
    {

        public int OverallWeight { get; set; }

        public Truck() : base(VehicleType.Truck)
        {
        }

        protected override void SetAdditionalInfo(string[] tokens)
        {
            int.TryParse(tokens[tokens.Length - 1], out var overallWeight);
            OverallWeight = overallWeight;
        }

        protected override string GetAdditionalInfo()
        {
            return $"{OverallWeight}";
        }
    }
}
