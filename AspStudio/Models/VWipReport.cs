using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class VWipReport
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string JobName { get; set; }
        public string AwardedTo { get; set; }

        public string Rep { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? FinalInvoiceDate { get; set; }
        public DateTime? JEIPMeetingDate { get; set; }
        public DateTime? PaidInFullDate { get; set; }
        public DateTime? CommPaidDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int? DaysWip { get; set; }
        public decimal? EagleBidSales { get; set; }
        public decimal? AmountDone { get; set; }
        public int? Mobilization { get; set; }
        public decimal? CollectedAmount { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public decimal? EagleBidPrice { get; set; }
        public string PercentageDone { get; set; }
        public int? Prep12 { get; set; }
        public int? Prep14 { get; set; }
        public int? Prep13 { get; set; }
        public int? PrepDone { get; set; }
        public int? Removal12 { get; set; }
        public int? Removal34 { get; set; }
        public int? DemoDone { get; set; }
        public DateTime? InvReturnDate { get; set; }
        public DateTime? EquipReturnDate { get; set; }
        public int? TotalComplete { get; set; }
        public DateTime? MobilizationDate { get; set; }
        public DateTime? Prep14Date { get; set; }
        public DateTime? Prep12Date { get; set; }
        public DateTime? PrepDoneDate { get; set; }
        public DateTime? Removal12Date { get; set; }
        public DateTime? DemoDoneDate { get; set; }
        public DateTime? Prep34Date { get; set; }
        public DateTime? RemovalDoneDate { get; set; }
        public DateTime? DateAddedStr { get; set; }

        public string? CustomerName { get; set; }
        public DateTime? CustomerDateCreated { get; set; }

        public string ProductType { get; set; }

        public string? ContactType { get; set; }
        public DateTime? ContactTypeDateCreated { get; set; }

        public decimal? CompetitorPrice { get; set; }
        public string MissedBy { get; set; }
        public string BidNumber { get; set; }


    }
}
