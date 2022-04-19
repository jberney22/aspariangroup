using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using EagleApp.Data;
using EagleApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace EagleApp.Service
{
    public class JobLogService
    {

        private readonly AspStudioIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
      

        public JobLogService(AspStudioIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        internal async Task<JobLogResponse> AddJobLogAsync(JobLog jobLogModel, bool isParent, string parentBidNumber)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                var ocawa = _context.Ocawas.Find(Convert.ToInt32(jobLogModel.ProjectOc));

                jobLogModel.DateAdded = DateTime.Now;
                jobLogModel.Status = "1"; //Pending Status

                _context.Add(jobLogModel);
                var result = await _context.SaveChangesAsync();
                if (!isParent)
                {
                    jobLogModel.BidNumber = $"{jobLogModel.Id}";
                    jobLogModel.ProjectNumber = $"{jobLogModel.Id}-{ocawa.Ocawaname} : {jobLogModel.JobName} : {jobLogModel.ClientName}";
                }
                else
                {
                    parentBidNumber = parentBidNumber.Substring(0, parentBidNumber.LastIndexOf("-") + 1).Replace("-","");
                    jobLogModel.BidNumber = parentBidNumber;
                    jobLogModel.ProjectNumber = $"{parentBidNumber}-{ocawa.Ocawaname} : {jobLogModel.JobName} : {jobLogModel.ClientName}";
                }

                var result2 = await _context.SaveChangesAsync(jobLogModel.UserId, jobLogModel.Id);
                response.Sucess = result2 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Sucess = false;
            }
            response.JobLog = jobLogModel;
            return response;
        }

        internal IQueryable<VWipReport> GetDataByDynamic(WIPReportModel model)
        {
            //string csv = String.Join(",", model.Values.Select(x => x.ToString()).ToArray());
            //var result = _context.VGetJobLog.Select(EagleApp.Helpers.Helpers.DynamicSelectGenerator<VGetJobLog>(csv)).ToList();
            //return result;

            var data = _context.Set<VWipReport>().FromSqlRaw("exec dbo.usp_GeneratedReport").AsEnumerable();
            if(!string.IsNullOrEmpty(model.Department))
            {
                data = data.Where(o=> o.Department.ToLower().Contains(model.Department.ToLower()));
            }

            if (!string.IsNullOrEmpty(model.Estimator))
            {
                data = data.Where(o => o.Rep.ToLower().Contains(model.Estimator.ToLower()));
            }

            if (!string.IsNullOrEmpty(model.ProjectOc))
            {
                data = data.Where(o => o.ProjectNumber.ToLower().Contains(model.ProjectOc.ToLower()));
            }

#if DEBUG
            if (model.StartDate != null && model.EndDate != null)
            {
                data = data.Where(o => o.StartDate >= model.StartDate && o.StartDate <= model.EndDate);
            }
#else
              if (model.StartDate != null && model.EndDate != null)
              {
                data = data.Where(o => o.StartDate.AddHours(-7) >= model.StartDate.AddHours(-7) && o.StartDate.AddHours(-7) <= model.EndDate.AddHours(-7));
              }
#endif


            return data.AsQueryable();
        }

        

        public List<string> GetDataColumns()
        {
            List<string> columnsList = new List<string>();
            var sql = @"SELECT COLUMN_NAME 
                        FROM information_schema.columns 
                        WHERE table_name = 'v_GetJobLogs' AND COLUMN_NAME <> 'Id'";

            _context.Database.ExecuteSqlRaw(sql);

            using SqlConnection myCon = new SqlConnection(_context.Database.GetConnectionString());
            myCon.Open();
            using SqlCommand myCommand = new SqlCommand(sql, myCon);
            using SqlDataReader rdr = myCommand.ExecuteReader();

            while (rdr.Read())
            {
                columnsList.Add(Convert.ToString(rdr["COLUMN_NAME"]));
            }

            return columnsList;

        }

        internal IQueryable<VGetJobLog> GetAllJobLogs()
        {
            // return _context.JobLog.ToList();
            return _context.VGetJobLog.OrderByDescending(o=>o.DateAdded).AsQueryable();
        }

        internal IQueryable<VWipReport> GetWIPReportData(string? date)
        {
            return _context.Set<VWipReport>().FromSqlRaw("exec dbo.sp_WipReport {0}", date);
        }

        private string GetStatusName(string status)
        {
            var jbstatus = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == status);
            return jbstatus.Status;

        }

        internal List<ContactType> GetContacts()
        {
            return _context.ContactTypes.ToList();
        }

        internal List<JobStatus> GetJobStatus()
        {
            return _context.JobStatus.ToList();
        }

        internal List<Ocawa> GetAllProjectOC()
        {
            return _context.Ocawas.ToList();
        }

        internal List<Department> GetAllDepartments()
        {
            return  _context.Departments.ToList();
        }

        internal string GetJobStatusIdByName(string statsName)
        {

            var obj = _context.JobStatus.FirstOrDefault(o => o.Status.ToLower() == statsName.ToLower());
            return obj.Id.ToString();
        }

        internal JobLog GetJobLogbyId(int id)
        {
            var obj = _context.JobLog.Find(id);
            var statusName = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == obj.Status);
            obj.Status = statusName.Status;
            return obj;
        }

        internal async Task<JobLogResponse> UpdateJobLog(JobLog jobLogModel, bool addAudit)
        {
            // var _context.JobLog.Update(jobLogModel);
            JobLogResponse response = new JobLogResponse();
            try
            {
                if (jobLogModel.AcceptedDate != null) jobLogModel.Status = "Accepted";
                if (jobLogModel.StartDate != null) jobLogModel.Status = "Started";
                if (jobLogModel.FinishDate != null) jobLogModel.Status = "Finished";
                if (jobLogModel.FinalInvoiceDate != null) jobLogModel.Status = "Invoiced";
                if (jobLogModel.CloseoutDoneDate != null) jobLogModel.Status = "Closed";
                if (jobLogModel.JeipmeetingDate != null) jobLogModel.Status = "JEIP";
                if (jobLogModel.PaidInFullDate != null) jobLogModel.Status = "Paid in Full";
                if (jobLogModel.CommPaidDate != null) jobLogModel.Status = "Comm Paid";

               // jobLogModel.ProjectOc = GetAllProjectOC().FirstOrDefault(obj => obj.Project)
                jobLogModel.Status = GetJobStatusIdByName(jobLogModel.Status);
                jobLogModel.DateModified = DateTime.Now;
                var job = await _context.JobLog.FindAsync(jobLogModel.Id);
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

                job.FinalInvoiceDate = jobLogModel.FinalInvoiceDate;

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

                job.TotalComplete = CalculateTotalComplete(jobLogModel); //jobLogModel.TotalComplete;

                _context.Update(job);
               // _context.Entry(oldProduct).CurrentValues.SetValues(jobLogModel);
               //  _context.JobLog.Update(jobLogModel);



                int result = 0;
                if (!addAudit)
                {
                    //jobLogModel.ProjectOc = _context.Ocawas.FirstOrDefault(obj => obj.Ocawaname == jobLogModel.ProjectOc).Id.ToString();
                    result = await _context.SaveChangesAsync();
                }
                else
                {
                    result = await _context.SaveChangesAsync(jobLogModel.UserId, jobLogModel.Id);
                }

                _context.Wips.Add(new Wip()
                {
                    DateAdded = DateTime.Now,
                    EquipReturnDate = jobLogModel.EquipReturnDate,
                    InvReturnDate = jobLogModel.InvReturnDate,
                    JoblogId = jobLogModel.Id,
                    Mobilization = jobLogModel.Mobilization,
                    MobilizationDate = jobLogModel.MobilizationDate,
                    Prep14 = jobLogModel.Prep14,
                    Prep14Date = jobLogModel.Prep14Date,
                    Prep12 = jobLogModel.Prep12,
                    Prep12Date = jobLogModel.Prep12Date,
                    Prep13 = jobLogModel.Prep34,
                    Prep34Date = jobLogModel.Prep34Date,
                    PrepDone = jobLogModel.PrepDone,
                    PrepDoneDate = jobLogModel.PrepDoneDate,
                    Removal12 = jobLogModel.Removal12,
                    Removal12Date = jobLogModel.Removal12Date,
                    Removal34 = jobLogModel.RemovalDone,
                    RemovalDoneDate = jobLogModel.RemovalDoneDate,
                  

                });


                result = await _context.SaveChangesAsync();

                //var result = await _context.SaveChangesAsync(jobLogModel.UserId, jobLogModel.Id);
                response.Sucess = result > 0 ? true : false;
                response.JobLog = jobLogModel;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Sucess = false;
            }
            return response;
        }

        private int? CalculateTotalComplete(JobLog jobLogModel)
        {
            //  int totalComplete = 0;
            var nums = new int?[] { jobLogModel.Mobilization,
                        jobLogModel.Prep14,
                        jobLogModel.Prep12,
                        jobLogModel.Prep34,
                        jobLogModel.PrepDone,
                        jobLogModel.Removal12,
                        jobLogModel.RemovalDone,
                        jobLogModel.DemoDone };

            var total = nums.Sum(i => i ?? 0);
            return total;
                        
        }

        internal async Task<JobLogResponse> DeleteJobLog(JobLog job)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                _context.JobLog.Remove(job);
                var result = _context.SaveChanges();
                response.Sucess = result == 1 ? true : false;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Sucess = false;
            }
            return response;
        }

        internal List<string> GetAllBidNumbers()
        {
            var jLogs = _context.JobLog.Where(o => o.BidNumber != null)
                                       .Where(o => o.BidNumber != "0")
                                       .OrderBy(o => o.BidNumber)
                                       .Select(p => p.BidNumber).Distinct().ToList();
            return jLogs;
            
        }
    }
}
