using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var data = jblogService.GetWIPReportData();

            var model = new WIPReportModel()
            {
                JobLog2s = data.ToList(),
                WIPSubTotalFormSales = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)),
                WIPSubTotalAmtDone = Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone)),
                WIPSubTotalAmtLeft = Convert.ToDecimal(data.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(data.Where(o => o.AmountDone != null).Sum(p => p.AmountDone))
             
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(WIPReportModel postModel)
        {
            var data = jblogService.GetWIPReportData().ToList();

            var newdata = data.Where(i => i.DateAddedString == postModel.PostDate.Value.ToShortDateString()).ToList();
                                                     

            var model = new WIPReportModel()
            {
                JobLog2s = newdata.ToList(),
                WIPSubTotalFormSales = Convert.ToDecimal(newdata.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)),
                WIPSubTotalAmtDone = Convert.ToDecimal(newdata.Where(o => o.AmountDone != null).Sum(p => p.AmountDone)),
                WIPSubTotalAmtLeft = Convert.ToDecimal(newdata.Where(o => o.EagleBidSales != null).Sum(p => p.EagleBidSales)) - Convert.ToDecimal(newdata.Where(o => o.AmountDone != null).Sum(p => p.AmountDone))

            };

            return View(model);
        }

    }
}
