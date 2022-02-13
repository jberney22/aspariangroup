using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Service
{
    public class StatusService
    {

        private readonly AspStudioIdentityDbContext _context;
      

        public StatusService(AspStudioIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        internal async Task<JobLogResponse> AddJobStatus(JobStatus jobStatus)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                _context.Add(jobStatus);
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

        internal List<JobStatus> GetAllJobStatus()
        {
            return  _context.JobStatus.ToList();
        }

        internal JobStatus GetJobStatusById(int id)
        {
            return _context.JobStatus.Find(id);
        }



        internal async Task<JobLogResponse> UpdateJobStatus(JobStatus jobStatus)
        {
            // var _context.JobLog.Update(jobLogModel);
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                _context.JobStatus.Update(jobStatus);
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

        internal object DeleteCustomer(int id)
        {
            var jobStatus = GetJobStatusById(id);
            _context.JobStatus.Remove(jobStatus);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
