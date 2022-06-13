using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Service
{
    public class SavedViewService
    {
        private readonly AspStudioIdentityDbContext _context;
        private readonly JobLogService _jobLogService;

        public SavedViewService(AspStudioIdentityDbContext context, JobLogService jobLogService)
        {
            _context = context;
            _jobLogService = jobLogService;
        }

        internal  AuditLogResponse AddSaveView(SavedViews savedViews)
        {
            AuditLogResponse response = new AuditLogResponse();
            try
            {
                _context.Add(savedViews);
                var result =  _context.SaveChanges();
                response.Sucess = result == 1 ? true : false;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Sucess = false;
            }
            return response;
        }

        //internal async Task<AuditCompareModel> GetAuditById(int id)
        //{
        //    var auditCompareModel = new AuditCompareModel();
        //    var obj = await _context.AuditLogs.FindAsync(id);
        //    auditCompareModel.OldModel = JsonConvert.DeserializeObject<JobLog>(obj.OldValues);
        //    auditCompareModel.NewModel = JsonConvert.DeserializeObject<JobLog>(obj.NewValues);
        //    auditCompareModel.OldModel.Id = auditCompareModel.NewModel.Id = obj.JobLogId;


        //    var statusNameOld = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Status);
        //    var statusNameNew = _context.JobStatus.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Status);

        //    var projectOldVal = _context.Ocawas.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.ProjectOc);
        //    var projectNewVal = _context.Ocawas.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.ProjectOc);

        //    var deptOldVal = _context.Departments.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Department);
        //    var deptNewVal = _context.Departments.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Department);

        //    var contactOldVal = _context.ContactTypes.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.OldModel.Contact);
        //    var contactNewVal = _context.ContactTypes.FirstOrDefault(o => o.Id.ToString() == auditCompareModel.NewModel.Contact);

        //    auditCompareModel.OldModel.Status = statusNameOld != null ? statusNameOld.Status : string.Empty;
        //    auditCompareModel.NewModel.Status = statusNameNew != null ? statusNameNew.Status : string.Empty;

         
        //    return auditCompareModel;
        //}

        internal List<SavedViews> GetAllSavedViews()
        {
            var query = _context.SavedViews.ToList();
            return query;
        }
    }
}
