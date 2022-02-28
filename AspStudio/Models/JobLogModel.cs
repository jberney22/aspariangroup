using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Models
{
    public class JobLogModel //: JobLog
    {
        public int Id { get; set; }
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

        [Required(ErrorMessage = "! Department Required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "! Estimator Required")]
        public string Rep { get; set; }

        [Required(ErrorMessage = "! Project Required")]
        public string ProjectOc { get; set; }

        [Required(ErrorMessage = "! Contact Required")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "! Product Type Required")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "! EagleBid Price Required")]
        public decimal? EagleBidPrice { get; set; }
        public string AwardedTo { get; set; }

        [Required(ErrorMessage = "! Competitor Price Required")]

        public decimal? CompetitorPrice { get; set; }
        
        public string MissedBy { get; set; }
        [Required(ErrorMessage = "! EagleBid Sales Required")]
        public decimal? EagleBidSales { get; set; }

        [Required(ErrorMessage = "! MissedBy Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public decimal MissedByDecimal { get; set; }
        public string FinalInvoice { get; set; }
        public decimal? CollectedAmount { get; set; }
        public string ProjectNumber { get; set; }
        public string Status { get; set; }
        public string JobFolderLink { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "! OpenDate Required")]
        [DataType(DataType.Date)]
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
        [Required(ErrorMessage = "! JobName Required")]
        public string JobName { get; set; }

        [Required(ErrorMessage = "! ClientName Required")]

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

    }
}
