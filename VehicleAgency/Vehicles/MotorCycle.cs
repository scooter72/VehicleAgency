using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency.Vehicles
{
    public class Motorcycle : Vehicle
    {

        public string VehicleCategory { get; set; }

        public Motorcycle() : base(VehicleType.Motorcycle)
        {
        }

        protected override void SetAdditionalInfo(string[] tokens)
        {
            VehicleCategory = tokens[tokens.Length - 1];
        }

        protected override string GetAdditionalInfo()
        {
            return $"{VehicleCategory}";
        }
    }
}
