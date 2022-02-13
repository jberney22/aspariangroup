using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class JobLogResponse
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }

        public JobLog JobLog { get; set; }
    }

    public class AuditLogResponse
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }

    }


}
