using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class AuditLogUserModel : AuditLog
    {
        public string User { get; set; }
    }
}
