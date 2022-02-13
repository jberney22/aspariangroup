using EagleApp.Models;
using EagleApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    public class StatusController : Controller
    {

        private readonly StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            _statusService = statusService;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = _statusService.GetAllJobStatus();
            return View(customers);
        }
        public ActionResult JobStatusList()
        {
            var customers = _statusService.GetAllJobStatus();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobStatus jobStatus)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                
                var result = await _statusService.AddJobStatus(jobStatus);
                return RedirectToAction(nameof(JobStatusList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _statusService.GetJobStatusById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobStatus jobStatus)
        {
            if (!ModelState.IsValid)
            {
                return View(jobStatus);
            }
            try
            {
                var response = _statusService.UpdateJobStatus(jobStatus);
                return RedirectToAction(nameof(JobStatusList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _statusService.DeleteCustomer(id);
            return RedirectToAction(nameof(JobStatusList));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
