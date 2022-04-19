using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class WIPReportModel
    {
        public WIPReportModel()
        {
            JobLog2s = new List<JobLog2>();
            VWipReport = new List<VWipReport>();
        }
        public decimal WIPSubTotalAmtLeft{ get; set; }
        public decimal WIPSubTotalAmtDone { get; set; }
        public decimal WIPSubTotalFormSales { get; set; }
        public List<JobLog2> JobLog2s { get; set; }
        public List<VWipReport> VWipReport { get; set; }
        public DateTime? PostDate { get; set; }

        public string Department { get; set; }
        public string ProjectOc { get; set; }
        public string Estimator { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }

   

    public class ListValues {
        public List<string> Values { get; set; }
    }
}
