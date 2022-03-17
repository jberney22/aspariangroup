using System;

namespace EagleApp.Models
{
    public class JobLog2 : JobLog
    {
        public string DaysWIP { get; set; }
        public double? AmountDone { get; set; }
        public string DateAddedString { get; set; }

        public DateTime? DateAddedMax { get; set; }

    }
}