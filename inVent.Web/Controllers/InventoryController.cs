using inVent.Models.InventoryModels;
using inVent.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inVent.Web.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            var service = CreateInventoryService();
            var model = service.GetAllInventory();
            return View(model);
        }
        private InventoryService CreateInventoryService()
        {
            var service = new InventoryService(Guid.Parse(User.Identity.GetUserId()));
            return service;
        }
        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            var service = CreateInventoryService();
            var model = service.GetFacilityInventory(id);
            return View(model);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInventoryService();

            if(service.CreateInventory(model))
            {
                TempData["SaveResult"] = "Inventory was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Inventory not created.");

            return View(model);
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/Edit/5
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

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
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
