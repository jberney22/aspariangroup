using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class DashBoardModel
    {

        public DashBoardModel()
        {
            VDashboardDatum = new List<VDashboardDatum>();
        }
        public decimal OpenBids { get; set; }
        public decimal ClosedBids { get; set; }
        public decimal Collected { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ViewType { get; set; }
        public string ViewDateType { get; set; }

        public List<VDashboardDatum> VDashboardDatum { get; set;}
    }
}
