using System;
using System.Collections.Generic;

namespace EagleApp.Models
{
    public partial class JobLog
    {
        public JobLog()
        {
            AuditLogs = new HashSet<AuditLog>();
            Wips = new HashSet<Wip>();
        }

        public int Id { get; set; }
        public string Department { get; set; }
        public string Rep { get; set; }
        public string ProjectOc { get; set; }
        public string Contact { get; set; }
        public string ProductType { get; set; }
        public decimal? EagleBidPrice { get; set; }
        public string AwardedTo { get; set; }
        public decimal? CompetitorPrice { get; set; }
        public string MissedBy { get; set; }
        public decimal? EagleBidSales { get; set; }
        public string FinalInvoice { get; set; }
        public decimal? CollectedAmount { get; set; }
        public string ProjectNumber { get; set; }
        public string Status { get; set; }
        public string JobFolderLink { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? JeipmeetingDate { get; set; }
        public DateTime? PaidInFullDate { get; set; }
        public DateTime? CommPaidDate { get; set; }
        public string BidNumber { get; set; }
        public bool Rejected { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? FinalInvoiceDate { get; set; }
        public DateTime? CloseoutDoneDate { get; set; }
        public string JobName { get; set; }
        public string ClientName { get; set; }
        public int? Mobilization { get; set; }
        public int? Prep14 { get; set; }
        public int? Prep12 { get; set; }
        public int? Prep34 { get; set; }
        public int? PrepDone { get; set; }
        public int? Removal12 { get; set; }
        public int? RemovalDone { get; set; }
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
        public string PercentageDone { get; set; }
        public DateTime? Prep34Date { get; set; }
        public DateTime? RemovalDoneDate { get; set; }

        //public virtual AspNetUser User { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Wip> Wips { get; set; }
    }
}
