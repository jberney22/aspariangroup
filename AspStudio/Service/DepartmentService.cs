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

namespace EagleApp.Service
{
    public class DepartmentService
    {

        private readonly AspStudioIdentityDbContext _context;
      

        public DepartmentService(AspStudioIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        internal async Task<JobLogResponse> AddDepartment(Department department)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                department.DateCreated = DateTime.Now;
                _context.Add(department);
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

        internal List<Department> GetAllDepartments()
        {
            return  _context.Departments.ToList();
        }

       

        internal async Task<JobLogResponse> UpdateDepartment(Department department)
        {
            // var _context.JobLog.Update(jobLogModel);
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                department.DateModified = DateTime.Now;
                _context.Departments.Update(department);
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

        internal Department GetDeptById(int id)
        {
            return _context.Departments.Find(id);
        }

        internal object DeleteDepartment(int id)
        {
            var dept = GetDeptById(id);
            _context.Departments.Remove(dept);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
