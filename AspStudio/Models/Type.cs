using System;
using System.Collections.Generic;

#nullable disable

namespace EagleApp.Models
{
    public partial class Type
    {
        public int Id { get; set; }
        public string Type1 { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
