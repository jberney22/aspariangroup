using System;
using System.Collections.Generic;

#nullable disable

namespace EagleApp.Models
{
    public partial class Wip2
    {
        public string Department { get; set; }
        public string Rep { get; set; }
        public string ProjectJobClient { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ScheduledFinishDate { get; set; }
        public string Status { get; set; }
        public string DaysInWip { get; set; }
        public double? ScheduledTimeExpired { get; set; }
        public decimal? EagleSBidFormSales { get; set; }
        public decimal? AmountDone { get; set; }
        public double? Done { get; set; }
        public bool _5MobilizationDone { get; set; }
        public bool _15Prep14 { get; set; }
        public bool _15Prep12 { get; set; }
        public bool _15Prep34 { get; set; }
        public bool _15PrepDone { get; set; }
        public bool _15Removal12 { get; set; }
        public bool _15RemovalDone { get; set; }
        public bool _5DemobDone { get; set; }
        public string F21 { get; set; }
        public bool InventoryReturned { get; set; }
        public bool EquipmentReturned { get; set; }
    }
}
