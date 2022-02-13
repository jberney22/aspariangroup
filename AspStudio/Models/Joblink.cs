using System;
using System.Collections.Generic;

#nullable disable

namespace EagleApp.Models
{
    public partial class Joblink
    {
        public string BidNumber { get; set; }
        public string Department { get; set; }
        public string Rep { get; set; }
        public string Client { get; set; }
        public string Job { get; set; }
        public string JobFolderLink { get; set; }
    }
}
