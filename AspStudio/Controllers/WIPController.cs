using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EagleApp.Helpers;
using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EagleApp.Controllers
{
    public class WIPController : Controller
    {
        private readonly JobLogService jblogService;
        private readonly DashboardService dashboardService;
        
        private readonly ILogger<WIPController> _logger;

        public WIPController(ILogger<WIPController> logger, JobLogService _jblogService,  DashboardService _dashboardService)
        {
            _logger = logger;
            dashboardService = _dashboardService;
            jblogService = _jblogService;
        }
        // GET: WIPController
        public ActionResult Index()
        {
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
        public ActionResult Index(WIPReportModel postModel)
        {
            List<VWipReport> data = null;
            
            if (postModel.PostDate != null) {
                var dateToPost = postModel.PostDate.Value.AddHours(-7).ToShortDateString();
                data = jblogService.GetWIPReportData(dateToPost).ToList();
            }
            else
                data = jblogService.GetWIPReportData(null).ToList();

           // var NoDuplicates = data.Distinct(new JobLogComparer());
             var NoDuplicates = data
                             .GroupBy(a => a.Id)
                             .Select(g => g.OrderByDescending(a => a.DateAddedStr).First())
                             .ToList();

          
            var model = new WIPReportModel()
            {
                VWipReport = NoDuplicates.ToList(),
                WIPSubTotalFormSales = Convert.ToDecimal(NoDuplicates.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)),
                WIPSubTotalAmtDone = Convert.ToDecimal(NoDuplicates.Where(o => o.AmountDone != null).Sum(p => p.AmountDone)),
                WIPSubTotalAmtLeft = Convert.ToDecimal(NoDuplicates.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(NoDuplicates.Where(o => o.AmountDone != null).Sum(p => p.AmountDone))

            };

            return View(model);
        }

    }
}
