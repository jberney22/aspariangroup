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
    public class CustomerController : Controller
    {

        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }
        public ActionResult CustomerList()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        public List<string> GetCustomerList(string name)
        {
            //var customers = _customerService.GetAllCustomers().Where(o=>o.CustomerName.Contains(name)).ToList();
            var customers = _customerService.GetAllCustomers();
            customers = customers.Where(r => customers.Any(f => r.CustomerName.ToLower().StartsWith(name.ToLower()))).ToList();
            return customers.Select(o => o.CustomerName).Distinct().ToList();
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
        public async Task<ActionResult> Create(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                
                var result = await _customerService.AddCustomerService(customer);
                return RedirectToAction(nameof(CustomerList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            try
            {
                var response = _customerService.UpdateCustomers(customer);
                return RedirectToAction(nameof(CustomerList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(CustomerList));
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
