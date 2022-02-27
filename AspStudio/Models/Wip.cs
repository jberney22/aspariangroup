using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class Wip
    {
        public int Wipid { get; set; }
        public int? Mobilization { get; set; }
        public DateTime? MobilizationDate { get; set; }
        public int? Prep14 { get; set; }
        public int? Prep12 { get; set; }
        public int? Prep13 { get; set; }
        public DateTime? Prep14Date { get; set; }
        public DateTime? Prep12Date { get; set; }
        public DateTime? Prep34Date { get; set; }
        public int? PrepDone { get; set; }
        public DateTime? PrepDoneDate { get; set; }
        public int? Removal12 { get; set; }
        public int? Removal34 { get; set; }
        public DateTime? Removal12Date { get; set; }
        public DateTime? RemovalDoneDate { get; set; }
        public DateTime? InvReturnDate { get; set; }
        public DateTime? EquipReturnDate { get; set; }
        public string TotalComplete { get; set; }
        public DateTime? DateAdded { get; set; }
        public int JoblogId { get; set; }

        public virtual JobLog Joblog { get; set; }
    }
}
