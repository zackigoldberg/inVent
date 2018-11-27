using inVent.Data;
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
    [Authorize]
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
            return View();
        }

        // GET: Facility/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFacilityService();

            if (service.CreateFacility(model))
            {
                TempData["SaveResult"] = "Your note was created";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created");

            return View(model);

        }

        // GET: Facility/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facility/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Facility/Delete/5
        public ActionResult Delete(int id)
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
