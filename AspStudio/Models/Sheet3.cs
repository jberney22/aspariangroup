using System;
using System.Collections.Generic;

#nullable disable

namespace EagleApp.Models
{
    public partial class Sheet3
    {
        public string Bid { get; set; }
        public string Department { get; set; }
        public string Rep { get; set; }
        public string Client { get; set; }
        public string Job { get; set; }
        public string ProjectOcAwa { get; set; }
        public string Contact { get; set; }
        public string ProductType { get; set; }
        public double? EagleSBidFormPrice { get; set; }
        public string AwardedTo { get; set; }
        public decimal? CompetitorPrice { get; set; }
        public string MissedBy { get; set; }
        public double? EagleSBidFormSales { get; set; }
        public DateTime? OpenDate { get; set; }
        public bool DeadRejected { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? FinalInvoicedDate { get; set; }
        public DateTime? CloseoutsDoneDate { get; set; }
        public DateTime? JeipMeetingDate { get; set; }
        public DateTime? PaidInFullDate { get; set; }
        public DateTime? CommPaidDate { get; set; }
        public string FinalInvoice { get; set; }
        public double? CollectedAmount { get; set; }
        public string ProjectJobClient { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
