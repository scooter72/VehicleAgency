using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Vehicles
{
    public class Truck : Vehicle
    {

        public int TotalWeight { get; set; }

        public Truck() : base(VehicleType.Truck)
        {
        }

        protected override void SetAdditionalInfo(string[] tokens)
        {
            int.TryParse(tokens[tokens.Length - 1], out var totalWeight);
            TotalWeight = totalWeight;
        }

        protected override string GetAdditionalInfo()
        {
            return $"{TotalWeight}";
        }
    }
}
