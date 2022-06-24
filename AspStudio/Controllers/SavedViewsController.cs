using EagleApp.Areas.Identity.Pages.Account;
using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    [Authorize(Roles = "Admin,superuser")]
    public class SavedViewsController : Controller
    {
        private readonly SavedViewService _savedViewService;
      

        public SavedViewsController(SavedViewService savedViewService)
        {
            _savedViewService = savedViewService;
        }
        public IActionResult Index()
        {
           var views =  _savedViewService.GetAllSavedViews();
            return View(views);
        }

       
        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
             _savedViewService.DeleteView(id);
            //return RedirectToAction(nameof(ListUsers));
            return RedirectToAction("Index");
        }

        
    }
}
