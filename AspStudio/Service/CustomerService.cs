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
    public class CustomerService
    {

        private readonly AspStudioIdentityDbContext _context;
      

        public CustomerService(AspStudioIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        internal async Task<JobLogResponse> AddCustomerService(Customer customer)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                customer.DateCreated = DateTime.Now;
                _context.Add(customer);
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

        internal List<Customer> GetAllCustomers()
        {
            return  _context.Customers.ToList();
        }

        internal Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }



        internal async Task<JobLogResponse> UpdateCustomers(Customer customer)
        {
            // var _context.JobLog.Update(jobLogModel);
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                customer.DateModified = DateTime.Now;
                _context.Customers.Update(customer);
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
            var customer = GetCustomerById(id);
            _context.Customers.Remove(customer);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
