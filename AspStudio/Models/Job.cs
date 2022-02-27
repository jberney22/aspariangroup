using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
