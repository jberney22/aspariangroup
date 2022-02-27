using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class BidLog
    {
        public double? OriginalOrder { get; set; }
        public double? Bid { get; set; }
        public string Department { get; set; }
        public string Rep { get; set; }
        public string Client { get; set; }
        public string Job { get; set; }
        public string ProjectOcAwa { get; set; }
        public string Contact { get; set; }
        public string ProductType { get; set; }
        public decimal? EagleSBidFormPrice { get; set; }
        public string AwardedTo { get; set; }
        public string CompetitorPrice { get; set; }
        public string MissedBy { get; set; }
        public string EagleSBidFormSales { get; set; }
        public string OpenDate { get; set; }
        public bool DeadRejected { get; set; }
        public string AcceptedDate { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string FinalInvoicedDate { get; set; }
        public string CloseoutsDoneDate { get; set; }
        public string JeipMeetingDate { get; set; }
        public string PaidInFullDate { get; set; }
        public string CommPaidDate { get; set; }
        public string FinalInvoice { get; set; }
        public decimal? CollectedAmount { get; set; }
        public string ProjectJobClient { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
