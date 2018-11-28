﻿using inVent.Data;
using inVent.Models.FacilityModels;
using inVent.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inVent.Web.Controllers
{

    public class FacilityController : Controller
    {
        // GET: Facility
        public ActionResult Index()
        {
            var service = CreateFacilityService();
            var model = service.GetFacilities();
            return View(model);
        }
        private FacilityService CreateFacilityService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FacilityService(userId);
            return service;
        }
        // GET: Facility/Details/5
        public ActionResult Details(int id)
        {
            var service = CreateFacilityService();
            var model = service.GetFacilityById(id);
            return View(model);
        }


        // GET: Facility/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFacilityService();

            if (service.CreateFacility(model))
            {
                TempData["SaveResult"] = "Facility was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Facility could not be created.");

            return View(model);

        }

        // GET: Facility/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = CreateFacilityService();
            var model = service.GetFacilityById(id);
            return View(model);
        }

        // POST: Facility/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FacilityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFacilityService();

            if (service.UpdateFacility(model))
            {
                TempData["SaveResult"] = "Facility was successfully Edited.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Facility could not be edited at this time.");

            return View(model);
        }

        // GET: Facility/Closure/5

        public ActionResult Close(int id)
        {
            return View();
        }

        // POST: Facility/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
