using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EagleApp.Controllers
{
    public class ReportGeneratorController : Controller
    {

        private readonly JobLogService jblogService;
        private readonly DashboardService dashboardService;

        private readonly ILogger<WIPController> _logger;

        public ReportGeneratorController(ILogger<WIPController> logger, JobLogService _jblogService, DashboardService _dashboardService)
        {
            _logger = logger;
            dashboardService = _dashboardService;
            jblogService = _jblogService;
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



            List<VWipReport> data = null;
            data = jblogService.GetWIPReportData(null).ToList();

            // var NoDuplicates =
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
            var model1 = new WIPReportModel()
            {
                VWipReport = data.ToList(),
                WIPSubTotalFormSales = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)),
                WIPSubTotalAmtDone = Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone)),
                WIPSubTotalAmtLeft = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone))

            };
            ModelState.Clear();
            return View(model1);
        }


    }
}
