using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EagleApp.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly AuditLogService _auditLogService;
        private readonly JobLogService _jobLogService;
        private readonly DepartmentService _deptService;

        public AuditLogController(AuditLogService auditLogService, JobLogService jobLogService, DepartmentService deptService)
        {
            _auditLogService = auditLogService;
            _jobLogService = jobLogService;
            _deptService = deptService;
        }
        // GET: AuditLogController
        public async Task<ActionResult> Index(int joblogId)
        {
            
            var auditLogs = _auditLogService.GetAllAuditLog().Where(o=>o.JobLog.Id == joblogId).ToList();
            auditLogs.ForEach(auditLog => {
                auditLog.DateCreated = TimeZoneInfo.ConvertTimeFromUtc(auditLog.DateCreated.Value, TimeZoneInfo.Local); //DateTime.SpecifyKind( DateTime.Parse(auditLog.DateCreated.ToString()),  DateTimeKind.Local);

            });
            
            return View(auditLogs);
        }

        // GET: AuditLogController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            AuditCompareModel auditCompareModel = await _auditLogService.GetAuditById(id);
            return View(auditCompareModel);
        }


        // POST: AuditLogController/Delete/5
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

        [HttpPost]
        public async Task<ActionResult> RevertData()
        {
            int id = 0;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["id"]))
                id = int.Parse(HttpContext.Request.Query["id"]);

            AuditCompareModel auditCompareModel = await _auditLogService.GetAuditById(id);

            var jobLog = await _jobLogService.UpdateJobLog(auditCompareModel.OldModel,false);
           // return View(auditCompareModel);
              return RedirectToAction(nameof(Index), new { joblogId = jobLog.JobLog.Id });
            //return Ok(jobLog.JobLog);
        }
    }
}
