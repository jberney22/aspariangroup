using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EagleApp.Controllers
{
    public class ReportGeneratorController : Controller
    {

        private readonly JobLogService jblogService;
        private readonly DashboardService dashboardService;
        private readonly SavedViewService _savedViewService;

        

        private readonly ILogger<WIPController> _logger;

        public ReportGeneratorController(ILogger<WIPController> logger, JobLogService _jblogService, DashboardService _dashboardService, SavedViewService savedViewService)
        {
            _logger = logger;
            dashboardService = _dashboardService;
            jblogService = _jblogService;
            _savedViewService = savedViewService;
        }
        // GET: WIPController
        public ActionResult Index()
        {

            //departments
            ViewBag.Columns = jblogService.GetDataColumns()
                    .Select(g => new SelectListItem
                    {
                        Value = g.ToString(),
                        Text = g.ToString()
                    }).ToList();



           // List<VWipReport> data = null;
           var  data = jblogService.GetDataByDynamic(new WIPReportModel()).ToList();




            ViewBag.Estimator = data.Where(o=>o.Rep != null)
                                    .GroupBy(a => a.Rep)
                                    .Select(g => new SelectListItem
                                    {
                                        Value = g.Key.ToString(),
                                        Text = g.Key.ToString()
                                    }).ToList();

            ViewBag.Departments = data.Where(o => o.Department != null)
                                  .GroupBy(a => a.Department)
                                  .Select(g => new SelectListItem
                                  {
                                      Value = g.Key.ToString(),
                                      Text = g.Key.ToString()
                                  }).ToList();


            ViewBag.Views = _savedViewService.GetAllSavedViews()
                            .Select(g => new SelectListItem
                            {
                                Value = g.Id.ToString(),
                                Text = g.ViewName.ToString()
                            }).ToList();


            var NoDuplicates = data
                             .GroupBy(a => a.Id)
                             .Select(g => g.OrderByDescending(a => a.DateAddedStr).First())
                             .ToList();


            // var NoDuplicates = data.Distinct(new JobLogComparer());
            var model = new WIPReportModel()
            {
                VWipReport = NoDuplicates.ToList(),
                WIPSubTotalFormSales = Convert.ToDecimal(NoDuplicates.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)),
                WIPSubTotalAmtDone = Convert.ToDecimal(NoDuplicates.Where(o => o.AmountDone != null).Sum(p => p.AmountDone)),
                WIPSubTotalAmtLeft = Convert.ToDecimal(NoDuplicates.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(NoDuplicates.Where(o => o.AmountDone != null).Sum(p => p.AmountDone))

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(WIPReportModel model)
        {
            
            var data = jblogService.GetDataByDynamic(model); 

            var dataColumns = jblogService.GetDataByDynamic(new WIPReportModel()).ToList();

             var NoDuplicates = data
                             .GroupBy(a => a.Id)
                             .Select(g => g.OrderByDescending(a => a.DateAddedStr).First())
                             .ToList();

              ViewBag.Estimator = dataColumns.Where(o=>o.Rep != null)
                                    .GroupBy(a => a.Rep)
                                    .Select(g => new SelectListItem
                                    {
                                        Value = g.Key.ToString(),
                                        Text = g.Key.ToString()
                                    }).ToList();

            ViewBag.Departments = dataColumns.Where(o => o.Department != null)
                                  .GroupBy(a => a.Department)
                                  .Select(g => new SelectListItem
                                  {
                                      Value = g.Key.ToString(),
                                      Text = g.Key.ToString()
                                  }).ToList();

            model.VWipReport = NoDuplicates.ToList();
            model.WIPSubTotalFormSales = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales));
            model.WIPSubTotalAmtDone = Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone));
            model.WIPSubTotalAmtLeft = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone));
            ModelState.Clear();
            return View(model);
        }


    }
}
