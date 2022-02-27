using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class AuditLog
    {
        public int AuditLogId { get; set; }
        public string LogAction { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserId { get; set; }
        public int JobLogId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string Type { get; set; }

        public virtual JobLog JobLog { get; set; }
      //  public virtual AspNetUser User { get; set; }
    }
}
