using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
