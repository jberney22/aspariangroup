﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Controllers
{
    public class FieldFlowController : Controller
    {
        // GET: FieldFlow
        public ActionResult Index()
        {
            return View();
        }

        // GET: FieldFlow/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FieldFlow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FieldFlow/Create
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

        // GET: FieldFlow/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FieldFlow/Edit/5
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

        // GET: FieldFlow/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FieldFlow/Delete/5
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
