using System;
using System.Collections.Generic;

#nullable disable

namespace EagleApp.Models
{
    public partial class Competitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
