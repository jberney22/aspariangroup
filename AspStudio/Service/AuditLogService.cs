using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Service
{
    public class AuditLogService
    {
        private readonly AspStudioIdentityDbContext _context;
        private readonly JobLogService _jobLogService;

        public AuditLogService(AspStudioIdentityDbContext context, JobLogService jobLogService)
        {
            _context = context;
            _jobLogService = jobLogService;
        }

        internal async Task<AuditLogResponse> AddAuditLog(AuditLog auditLog)
        {
            AuditLogResponse response = new AuditLogResponse();
            try
            {
                auditLog.DateCreated = DateTime.Now;
                _context.Add(auditLog);
                var result = await _context.SaveChangesAsync();
                response.Sucess = result == 1 ? true : false;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Sucess = false;
            }
            return response;
        }

        internal async Task<AuditCompareModel> GetAuditById(int id)
        {
            var auditCompareModel = new AuditCompareModel();
            var obj = await _context.AuditLogs.FindAsync(id);
            auditCompareModel.OldModel = JsonConvert.DeserializeObject<JobLog>(obj.OldValues);
            auditCompareModel.NewModel = JsonConvert.DeserializeObject<JobLog>(obj.NewValues);
            auditCompareModel.OldModel.Id = auditCompareModel.NewModel.Id = obj.JobLogId;


            var statusNameOld = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Status);
            var statusNameNew = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Status);

            var projectOldVal = _context.Ocawas.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.ProjectOc);
            var projectNewVal = _context.Ocawas.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.ProjectOc);

            var deptOldVal = _context.Departments.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Department);
            var deptNewVal = _context.Departments.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Department);

            var contactOldVal = _context.ContactTypes.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Contact);
            var contactNewVal = _context.ContactTypes.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Contact);

            auditCompareModel.OldModel.Status = statusNameOld != null ? statusNameOld.Status : string.Empty;
            auditCompareModel.NewModel.Status = statusNameNew != null ? statusNameNew.Status : string.Empty;

            //auditCompareModel.OldModel.ProjectOc = projectOldVal != null ? projectOldVal.Ocawaname : string.Empty; // projectOldVal.Ocawaname;
            //auditCompareModel.NewModel.ProjectOc = projectNewVal != null ? projectNewVal.Ocawaname : string.Empty;  //projectNewVal.Ocawaname;

            //auditCompareModel.OldModel.Department = deptOldVal != null ? deptOldVal.Name : string.Empty;
            //auditCompareModel.NewModel.Department = deptNewVal != null ? deptNewVal.Name : string.Empty;

            //auditCompareModel.OldModel.Contact = contactOldVal != null ? contactOldVal.Type : string.Empty;
            //auditCompareModel.NewModel.Contact = contactNewVal != null ? contactNewVal.Type : string.Empty;
            return auditCompareModel;
        }

        internal List<AuditLogUserModel> GetAllAuditLog()
        {
            var query = _context.AuditLogs    // your starting point - table in the "from" statement
                       .Join(_context.Users, // the source table of the inner join
                          logs => logs.UserId,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                          appUser => appUser.Id,   // Select the foreign key (the second part of the "on" clause)
                                                   // (logs, appUser) => new { Post = post, Meta = meta }) // selection
                         (logs, appUser) => new { AuditLogs = logs, User = appUser })
                          .Select(post => new AuditLogUserModel() { 
                            User=post.User.FirstName + " " + post.User.LastName,
                            DateCreated = post.AuditLogs.DateCreated,
                            LogAction = post.AuditLogs.LogAction,
                            JobLog = post.AuditLogs.JobLog,
                            AuditLogId = post.AuditLogs.AuditLogId
                          });
            return query.OrderByDescending(o=>o.DateCreated).ToList();
        }
    }
}
