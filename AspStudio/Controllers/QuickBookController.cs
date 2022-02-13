using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    public class QuickBookController : Controller
    {
        // GET: QuickBookController
        public ActionResult Index()
        {
             return View();
        }

        // GET: QuickBookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuickBookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuickBookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: QuickBookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuickBookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: QuickBookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuickBookController/Delete/5
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
