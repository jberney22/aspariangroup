using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EagleApp.Models;
using Microsoft.AspNetCore.Authorization;
using EagleApp.Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EagleApp.Controllers
{
 [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DashboardService _dashboardService;
        private readonly StatusService _statusService;
        public HomeController(ILogger<HomeController> logger, DashboardService dashboardService, StatusService statusService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
            _statusService = statusService;
        }

        public IActionResult Index()
        {
          
            GetViewDateType();
            List<JobStatus> jobStatuses = _statusService.GetAllJobStatus();
            ViewBag.ViewType = jobStatuses
                               .Select(g => new SelectListItem
                               {
                                   Value = g.Status.ToString(),
                                   Text = g.Status.ToString(),
                               }).ToList();
            var dashboard_data = _dashboardService.GetDashboardDataByViewType("Open Bids");

            return View(dashboard_data);
        }

        public string GetDateFromDateTime(DateTime datevalue)
        {
            return datevalue.ToShortDateString();
        }

        private void GetViewType()
        {
            //List<string> list1 = new List<string>() { "ALL","Open Bids", "Closed Bids", "Complete Jobs" };
            //SelectList list = new SelectList(list1);
            //ViewBag.ViewType = list;

            List<JobStatus> jobStatuses = _statusService.GetAllJobStatus();
            ViewBag.ViewType = jobStatuses
                               .Select(g => new SelectListItem
                               {
                                   Value = g.Status.ToString(),
                                   Text = g.Status.ToString(),
                               }).ToList();
        }
        private void GetViewDateType()
        {
            List<string> dateTypeList = new List<string>() { "Week", "Month", "Year" };
            SelectList list = new SelectList(dateTypeList);
            ViewBag.Current = list;
        }

        public IActionResult GetData(string datatype)
        {
            ViewBag.Current = datatype;
            GetViewType();
            var dashboard_data = _dashboardService.GetDashboardData(datatype);
            return View(nameof(Index), dashboard_data);
        }

        public IActionResult FilterReport(DashBoardModel model)
        {
            GetViewType();
            GetViewDateType();
            var dashboard_data = _dashboardService.GetDashboardDataByCriteria(model.StartDate, model.EndDate, model.ViewType, model.ViewDateType);
            return View(nameof(Index), dashboard_data);
        }

        public IActionResult ViewReport(DashBoardModel model)
        {

            var dashboard_data = _dashboardService.GetDashboardDataByViewType(model.ViewType);
            List<string> list1 = new List<string>() { "Open Bids", "Closed Bids", "Complete Jobs" };
            ViewBag.ViewType = list1
                  .Select(g => new SelectListItem
                  {
                      Value = g.ToString(),
                      Text = g.ToString(),
                      Selected = (g == dashboard_data.ViewType)
                  }).ToList();
            return View(nameof(Index), dashboard_data);
        }
        //return RedirectToRoute("default",new { controller = "Home", action = "Privacy" });
        //return View(dashboard_data);


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}
