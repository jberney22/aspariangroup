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
    public class DepartmentsController : Controller
    {

        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: DepartmentsController
        public ActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        public ActionResult DepartmentList()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        // GET: DepartmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var response = _departmentService.AddDepartment(department);
                return RedirectToAction(nameof(DepartmentList));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _departmentService.GetDeptById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: DepartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {

                var deptObj = _departmentService.GetDeptById(id);
                deptObj.Name = department.Name;
                var response = _departmentService.UpdateDepartment(deptObj);
                return RedirectToAction(nameof(DepartmentList));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _departmentService.DeleteDepartment(id);
            return RedirectToAction(nameof(DepartmentList));
        }

        // POST: DepartmentsController/Delete/5
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
