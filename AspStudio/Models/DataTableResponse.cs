using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class DataTableResponse
    {
        public int Draw { get; set; }
        public long RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object[] Data { get; set; }
        public string Error { get; set; }
    }

    public class JobLogDTO
    {
        public string ProjectNumber { get; set; }
        public string BidNumber { get; set; }
        public string DeptName { get; set; }

    }
}
