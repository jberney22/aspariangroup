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
        public List<string> Department2 { get; set; }
        public string ProjectOc { get; set; }
        public string Estimator { get; set; }
        public List<string> Estimator2 { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishDtStart { get; set; }
        public DateTime? FinishDtEnd { get; set; }

        public DateTime? OpenDtStart { get; set; }
        public DateTime? OpenDtEnd { get; set; }

        public string? CustomerName { get;set; }
        public DateTime? CustomerDateCreated { get; set; }
        public string? ContactType { get; set; }

        public DateTime? ContactTypeDateCreated { get; set; }

        public string? Status { get; set; }

        public string? ViewName { get; set; }
        public string? ViewNameSelected { get; set; }



    }

   

    public class ListValues {
        public List<string> Values { get; set; }
    }
}
