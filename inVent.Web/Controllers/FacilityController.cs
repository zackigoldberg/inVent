﻿using inVent.Data;
using inVent.Models.FacilityModels;
using inVent.Services;
using inVent.Web.Models;
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
            var role = User.IsInRole("Admin");
            var manager = User.IsInRole("Sales Manager");
            var service = new FacilityService(userId, role, manager);
            return service;
        }
        // GET: Facility/Details/5
        public ActionResult Details(int id)
        {
            var service = CreateFacilityService();
            var model = service.GetFacilityById(id);
            model.Inventories = service.Inventories(id);
            model.Sales = service.SalesByFacilityId(id);
            return View(model);
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
                TempData["SaveResult"] = "Facility was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Facility could not be created.");

            return View(model);

        }

        // GET: Facility/Edit/5
        
        public ActionResult Edit(int id)
        {
            var service = CreateFacilityService();
            var detail = service.GetFacilityById(id);
            var model =
                new FacilityEdit
                {
                    FacilityId = detail.FacilityId,
                    Name = detail.Name,
                    Type = detail.Type
                };
            return View(model);
        }
        // POST: Facility/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FacilityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.FacilityId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }


            var service = CreateFacilityService();

            if (service.UpdateFacility(model))
            {
                TempData["SaveResult"] = "Facility was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Facility could not be updated.");
            return View(model);
        }

        // GET: Facility/Close/5
        public ActionResult Close(int id)
        {
            var service = CreateFacilityService();
            var model = service.GetFacilityById(id);
            return View(model); 
        }

        // POST: Facility/CloseFacility/5
        [HttpPost]
        [ActionName("Close")]
        [ValidateAntiForgeryToken]
        public ActionResult CloseFacility(int id, FacilityDetail model)
        {
            {
                var service = CreateFacilityService();
                if (!ModelState.IsValid) return View();

                if (service.CloseFacility(id))
                {
                    TempData["SaveResult"] = "Facility was closed.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Facility could not be updated.");
                return View(model);
            }
        }
        //GET: Facility/ClosedFacilitiesList
        public ActionResult ClosedFacilitiesList()
        {
            var service = CreateFacilityService();
            var model = service.GetClosedFacilities();
            return View(model);
        }
    }
}
