using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class JobLogModel : JobLog
    {
        public string ParentBidNumber { get; set; }

        public bool IsCheckedMobilazation { get; set; }
        public bool IsCheckedPrep14 { get; set; }
        public bool IsCheckedPrep12 { get; set; }
        public bool IsCheckedPrep34 { get; set; }

        public bool IsPrepDone { get; set; }
        public bool IsRemoval12 { get; set; }
        public bool IsRemoval34 { get; set; }
        public bool IsDemoDone { get; set; }
        public bool IsInvReturnDate { get; set; }
        public bool IsEquipReturnDate { get; set; }
        public bool IsTotalComplete { get; set; }

    }
}
