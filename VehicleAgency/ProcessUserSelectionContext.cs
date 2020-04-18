using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAgency
{
    public class ProcessUserSelectionContext
    {
        public VehiclesManager VehiclesManager { get; set; }
        public UserSelectionMenuOptions Selection { get; set; }
        public string DataFilePath { get; set; }
        public Func<Vehicle> VehicleInfoInput { get; set; }
        public Func<string> LicensePlateInput { get; set; }
        public Func<string> ManufacturerInput { get; set; }
        public Func<int> ProductionYearInput { get; set; }

        public Func<int> SearchCriteriaInput { get; set; }
        public Func<int> SortCriteriaInput { get; set; }
    }
}
