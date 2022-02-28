using AutoMapper;
using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    [Authorize]
    public class JobLogController : Controller
    {
        private readonly JobLogService _jobLogService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DashboardService _dashboardService;
        private readonly CustomerService _customerService;
        private readonly AuditLogService _auditLogService;
        private readonly IMapper _mapper;
        private readonly StatusService _statusService;




        public JobLogController(JobLogService jobLogService, 
                                UserManager<ApplicationUser> userManager, 
                                DashboardService dashboardService,
                                AuditLogService auditLogService,
                                CustomerService customerService,
                                StatusService statusService,
                                IMapper mapper)
        {
            _jobLogService = jobLogService;
            _userManager = userManager;
            _dashboardService = dashboardService;
            _customerService = customerService;
            _auditLogService = auditLogService;
            _statusService = statusService;
            _mapper = mapper;
        }

        // GET: JobLogController
        public ActionResult Index()
        {
            List<JobStatus> jobStatuses = _statusService.GetAllJobStatus();
            ViewBag.ViewType = jobStatuses
                               .Select(g => new SelectListItem
                               {
                                   Value = g.Status.ToString(),
                                   Text = g.Status.ToString(),
                               }).ToList();

            return View();
        }

        public ActionResult GetJobLogs(string clientName)
        {

           var list = _jobLogService.GetAllJobLogs().Where(obj => obj.ProjectNumber.Contains(clientName))
                                                    .Select(x => new { 
                                                      Name = $"{x.BidNumber}-{x.ProjectOc}"
                                                    }).Distinct().ToList();
            return Json(list);
           
        }

        [HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetAllJobLogs()
        {
            var list = _jobLogService.GetAllJobLogs().Where(o => o.OpenDate != null).ToList();
            var newList = list.Select(item =>
                 new JobLogDTO
                 {
                     BidNumber = item.BidNumber,
                      DeptName = item.DeptName,
                      ProjectNumber = item.ProjectNumber
                 }).ToList();
            return new DataTableResponse
            {
                RecordsTotal = list.Count(),
                RecordsFiltered = 10,
                Data = list.ToArray()
            };
        }

      

        [HttpPost]
      //  [Route("get-data-criteria")]
        public async Task<ActionResult<DataTableResponse>> GetDataCriteria(DashBoardModel model)
        {
            DashBoardModel list = new DashBoardModel();
            List<VGetJobLog> rtnList = new List<VGetJobLog>();
            //    var list = _dashboardService.GetDashboardDataByCriteria(model.StartDate, model.EndDate, model.ViewType, model.ViewDateType);
            if (model.ViewType.ToLower() == "all".ToLower())
            {
                rtnList = _jobLogService.GetAllJobLogs().Where(o => o.Status != "Rejected").Where(o => o.OpenDate != null).ToList();
            }
            else
            {
                list = _dashboardService.GetDashboardDataByCriteria(model.StartDate, model.EndDate, model.ViewType, model.ViewDateType);
                foreach (var item in list.VDashboardDatum)
                {
                    VGetJobLog obj = new VGetJobLog();
                    obj.AcceptedDate = item.AcceptedDate;
                    obj.AwardedTo = item.AwardedTo;
                    obj.BidNumber = item.BidNumber;
                    obj.ClosedDate = item.ClosedDate;
                    obj.CloseoutDoneDate = item.CloseoutDoneDate;
                    obj.CollectedAmount = item.CollectedAmount;
                    obj.CommPaidDate = item.CommPaidDate;
                    obj.CompetitorPrice = item.CompetitorPrice;
                    obj.ContactType = item.Contact;
                    obj.EstimatorName = item.EstimatorName;
                    // obj.DateModified = item.Da;
                    obj.DeptName = item.Department;
                    obj.EagleBidPrice = item.EagleBidPrice;
                    obj.EagleBidSales = item.EagleBidSales;
                    obj.FinalInvoice = item.FinalInvoice;
                    obj.FinalInvoiceDate = item.FinalInvoiceDate;
                    obj.FinishDate = item.FinishDate;
                    obj.InvoiceDate = item.InvoiceDate;

                    obj.JeipmeetingDate = item.JeipmeetingDate;
                    obj.JobFolderLink = item.JobFolderLink;
                    obj.JobName = item.JobName;
                    obj.MissedBy = item.MissedBy;
                    obj.Notes = item.Notes;
                    obj.OpenDate = item.OpenDate;
                    obj.PaidInFullDate = item.PaidInFullDate;
                    obj.ProductType = item.ProductType;
                    obj.ProjectNumber = item.ProjectNumber;
                    obj.ProjectOc = item.ProjectOc;
                    obj.Rejected = item.Rejected;
                    obj.Rep = item.Rep;
                    obj.StartDate = item.StartDate;
                    obj.Status = item.Status;
                    obj.Id = item.Id;

                    rtnList.Add(obj);

                }
            }

            return new DataTableResponse
            {
                RecordsTotal = rtnList.Count(),
                RecordsFiltered = 100,
                Data = rtnList.ToArray()
            };
        }

        // GET: JobLogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobLogController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.departments = _jobLogService.GetAllDepartments();
            ViewBag.ProjectOc = _jobLogService.GetAllProjectOC();
            ViewBag.JobStatus = _jobLogService.GetJobStatus();
            ViewBag.Contact = _jobLogService.GetContacts();
            var users = await _userManager.GetUsersInRoleAsync("Estimator");
            ViewBag.Users = users.Select(o => new UsersModel()
            {
                FullName = o.FirstName + " " + o.LastName,
                UserId = o.Id
            }).Select(g => new SelectListItem
            {
                Value = g.UserId.ToString(),
                Text = g.FullName
            }).ToList();
            //ViewBag.Users = users.Select(o => new UsersModel()
            //{
            //    FullName = o.FirstName + " " + o.LastName,
            //    UserId = o.Id
            //}).ToList();

            ViewBag.ProjectBids = _jobLogService.GetAllBidNumbers();
            return View();
        }

        

        // POST: JobLogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        public async Task<ActionResult> Create(JobLogModel jobLogModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                jobLogModel.UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                //Check Client Name
                var customerObj =_customerService.GetAllCustomers().FirstOrDefault(p => p.CustomerName == jobLogModel.ClientName);
                if(customerObj == null)
                {
                    var cust = new Customer()
                    {
                        CustomerName = jobLogModel.ClientName,
                        Status = "Active",
                        CustomerClass = "LOCAL 2",
                        Currency = "US",
                        Terms = "DUR",
                        DateCreated = DateTime.Now
                    };
                    await _customerService.AddCustomerService(cust);
                }



                bool isParent = !string.IsNullOrEmpty(jobLogModel.ParentBidNumber) ? true : false;

                var job = _mapper.Map<JobLog>(jobLogModel);
                var result = await _jobLogService.AddJobLogAsync(job, isParent, jobLogModel.ParentBidNumber);
                //await _auditLogService.AddAuditLog(new AuditLog() { DateCreated = DateTime.Now, LogAction = $"Created {jobLogModel.ProjectNumber}", UserId = jobLogModel.UserId, JobLogId = result.JobLog.Id });
                return RedirectToAction(nameof(Edit), new { @Id = result.JobLog.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: JobLogController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            JobLog job = _jobLogService.GetJobLogbyId(id);

           //  job.MissedBy = job.MissedBy.Replace("%", "");
           // var inputValue = Math.Round(Convert.ToDecimal(job.MissedBy), 2);
           // job.MissedBy = string.Format("{0:P2}", Convert.ToDecimal(job.MissedBy));
            
            ViewBag.Message = "";

            
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();

            ViewBag.CurrentRole = roles;


            //departments
            ViewBag.departments = _jobLogService.GetAllDepartments()
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name.ToString(),
                        Selected = (g.Name == job.Department)
                    }).ToList();
            //ProjectOc
            ViewBag.ProjectOc = _jobLogService.GetAllProjectOC()
                   .Select(g => new SelectListItem
                   {
                       Value = g.Id.ToString(),
                       Text = g.Ocawaname,
                       Selected = (g.Id.ToString() == job.ProjectOc)
                   }).ToList();

            //Job Status
            ViewBag.JobStatus = _jobLogService.GetJobStatus()
                   .Select(g => new SelectListItem
                   {
                       Value = g.Id.ToString(),
                       Text = g.Status,
                       Selected = (g.Status == job.Status)
                   }).ToList();

            //Contact
            ViewBag.Contact = _jobLogService.GetContacts()
                   .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Type,
                        Selected = (g.Id.ToString() == job.Contact)
                    }).ToList();

            //Users
            var users = await _userManager.GetUsersInRoleAsync("Estimator");
            ViewBag.Users = users.Select(o => new UsersModel()
                            {
                                FullName = o.FirstName + " " + o.LastName,
                                UserId = o.Id
                            }).Select(g => new SelectListItem
                            {
                                Value = g.UserId.ToString(),
                                Text = g.FullName,
                                Selected = (g.FullName.ToLower() == job.Rep.ToLower())
                            }).ToList();

            //Awarded To
            List<string> list1 = new List<string>() { "Eagle", "Other"};
            ViewBag.AwardedTo = list1
                  .Select(g => new SelectListItem
                  {
                      Value = g.ToString(),
                      Text = g.ToString(),
                      Selected = (g == job.AwardedTo)
                  }).ToList();

            //Awarded To
            List<string> productType = new List<string>() { "Asbestos", "Lead", "Other" };
            ViewBag.ProductType = productType
                  .Select(g => new SelectListItem
                  {
                      Value = g.ToString(),
                      Text = g.ToString(),
                      Selected = (g == job.ProductType)
                  }).ToList();

            JobLogModel jobModel = _mapper.Map<JobLogModel>(job);
            jobModel.MissedByDecimal = Convert.ToDecimal(job.MissedBy);
            jobModel.IsCheckedMobilazation = job.MobilizationDate != null ? true : false;
            jobModel.IsCheckedPrep12 = job.Prep12Date != null ? true : false;
            jobModel.IsCheckedPrep14 = job.Prep14Date != null ? true : false;
            jobModel.IsCheckedPrep34 = job.Prep34 != null ? true : false;
            jobModel.IsPrepDone = job.PrepDoneDate != null ? true : false;
            jobModel.IsRemoval12 = job.Removal12Date != null ? true : false;
            jobModel.IsRemoval34 = job.RemovalDoneDate != null ? true : false;
            jobModel.IsDemoDone = job.DemoDoneDate != null ? true : false;

            jobModel.IsInvReturnDate = job.InvReturnDate != null ? true : false;
            jobModel.IsEquipReturnDate = job.EquipReturnDate != null ? true : false;
            jobModel.IsTotalComplete = job.TotalComplete != null ? true : false;



            return View(jobModel);
        }

        // POST: JobLogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, JobLogModel jobLogModel)
        {

            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return View(jobLogModel);
            }

            try
            {
                //jobLogModel.MissedBy = jobLogModel.MissedBy.Replace("%", "");
               // var tempMissed = double.Parse(jobLogModel.MissedBy.Replace("%", "")) / 100;
               // jobLogModel.MissedBy = tempMissed.ToString();   
                //JobLog job = _jobLogService.GetJobLogbyId(id);
                var job = _mapper.Map<JobLog>(jobLogModel);
                job.MissedBy = Convert.ToString(jobLogModel.MissedByDecimal);

                if (job == null)
                {
                    ViewBag.Message = "failed";
                    return View();
                }

                #region OLD CODE
                /*
                job.ClientName = jobLogModel.ClientName;
                job.Department = jobLogModel.Department;
                job.AwardedTo = jobLogModel.AwardedTo;
                job.CollectedAmount = jobLogModel.CollectedAmount;
                job.CompetitorPrice = jobLogModel.CompetitorPrice;
                job.Contact = jobLogModel.Department;
                job.EagleBidPrice = jobLogModel.EagleBidPrice;
                job.EagleBidSales = jobLogModel.EagleBidSales;
                job.FinalInvoice = jobLogModel.FinalInvoice;

                job.JobFolderLink = jobLogModel.JobFolderLink;
                job.MissedBy = jobLogModel.MissedBy;
                job.Notes = jobLogModel.Notes;

                job.ProductType = jobLogModel.ProductType;
                job.ProjectNumber = jobLogModel.ProjectNumber;
                job.ProjectOc = jobLogModel.ProjectOc;

                job.Rep = jobLogModel.Rep;
                
                job.Contact = jobLogModel.Contact;
                job.BidNumber = jobLogModel.BidNumber;

                job.JobFolderLink = jobLogModel.JobFolderLink;

                job.ProjectNumber = jobLogModel.ProjectNumber;
                job.ProjectOc = jobLogModel.ProjectOc;
                job.Rejected = jobLogModel.Rejected;
                job.StartDate = jobLogModel.StartDate;
                job.Status = jobLogModel.Status;

                job.CommPaidDate = jobLogModel.CommPaidDate;
                job.ClosedDate = jobLogModel.ClosedDate;
                job.JeipmeetingDate = jobLogModel.JeipmeetingDate;
                job.FinishDate = jobLogModel.FinishDate;
                job.OpenDate = jobLogModel.OpenDate;
                job.PaidInFullDate = jobLogModel.PaidInFullDate;
                job.InvoiceDate = jobLogModel.InvoiceDate;
                job.AcceptedDate = jobLogModel.AcceptedDate;
                job.Rejected = jobLogModel.Rejected;
                job.JobName = jobLogModel.JobName;

                // WIP
                job.Mobilization = jobLogModel.Mobilization;
                job.MobilizationDate = jobLogModel.MobilizationDate;
                job.Prep12 = jobLogModel.Prep12;
                job.Prep12Date = jobLogModel.Prep12Date;
                job.Prep14 = jobLogModel.Prep14;
                job.Prep14Date = jobLogModel.Prep14Date;
                job.Prep34 = jobLogModel.Prep34;
                job.Prep34Date = jobLogModel.Prep34Date;
                job.PrepDone = jobLogModel.PrepDone;
                job.PrepDoneDate = jobLogModel.PrepDoneDate;

                job.DemoDone = jobLogModel.DemoDone;
                job.DemoDoneDate = jobLogModel.DemoDoneDate;

                job.InvReturnDate = jobLogModel.InvReturnDate;
                job.Removal12 = jobLogModel.Removal12;
                job.Removal12Date = jobLogModel.Removal12Date;
                job.RemovalDone = jobLogModel.RemovalDone;
                job.EquipReturnDate = jobLogModel.EquipReturnDate;
                job.PercentageDone = jobLogModel.PercentageDone;

                job.TotalComplete = jobLogModel.TotalComplete;
                */
                #endregion

                
                // jobModel.Status = _jobLogService.GetJobStatus().FirstOrDefault(o => o.Status.ToLower() == jobLogModel.Status.ToLower()).Id.ToString();
                job.UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var jobLogResponse = await _jobLogService.UpdateJobLog(job,true);

                if (!jobLogResponse.Sucess)
                {
                    ViewBag.Message = "failed";
                    return View();
                    
                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: JobLogController/Delete/5
        public ActionResult Delete(int id)
        {
       
            JobLog job = _jobLogService.GetJobLogbyId(id);
            var result = _jobLogService.DeleteJobLog(job);
            return RedirectToAction(nameof(Index));
            
        }

        // POST: JobLogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
