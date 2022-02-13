using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Service
{
    public class TypeService
    {

        private readonly AspStudioIdentityDbContext _context;
      

        public TypeService(AspStudioIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        internal async Task<JobLogResponse> AddProductType(Models.Type type)
        {
            JobLogResponse response = new JobLogResponse();
            try
            {
                //var user = await GetCurrentUserAsync();
                type.DateCreated = DateTime.Now;
                _context.Add(type);
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

        internal List<Models.Type> GetAllProductType()
        {
            return  _context.Types.ToList();
        }

        internal Models.Type GetProductTypeById(int id)
        {
            return _context.Types.Find(id);
        }



        internal async Task<JobLogResponse> UpdateProductType(Models.Type type)
        {
            
            JobLogResponse response = new JobLogResponse();
            try
            {
                type.DateModified = DateTime.Now;
                _context.Types.Update(type);
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

        internal object DeleteProductType(int id)
        {
            var typeObj = GetProductTypeById(id);
            _context.Types.Remove(typeObj);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
