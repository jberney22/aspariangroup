using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class AuditCompareModel
    {
        public JobLog OldModel { get; set; }
        public JobLog NewModel { get; set; }

        public Dictionary<string, string> QueryString { get; set; }
    }
}
