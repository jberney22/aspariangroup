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
    public class TypeController : Controller
    {

        private readonly TypeService _prodTypeService;

        public TypeController(TypeService statusService)
        {
            _prodTypeService = statusService;
        }
        // GET: CustomerController
        
        public ActionResult ProductTypeList()
        {
            var customers = _prodTypeService.GetAllProductType();
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
        public async Task<ActionResult> Create(Models.Type type)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                
                var result = await _prodTypeService.AddProductType(type);
                return RedirectToAction(nameof(ProductTypeList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _prodTypeService.GetProductTypeById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Models.Type type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }
            try
            {
                var response = _prodTypeService.UpdateProductType(type);
                return RedirectToAction(nameof(ProductTypeList));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _prodTypeService.DeleteProductType(id);
            return RedirectToAction(nameof(ProductTypeList));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Models.Type type)
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
