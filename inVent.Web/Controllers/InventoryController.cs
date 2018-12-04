using inVent.Data;
using inVent.Models.InventoryModels;
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
    public class InventoryController : Controller
    {
       private ApplicationDbContext db = new ApplicationDbContext(); 
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
        // GET: Inventory/Details/
        public ActionResult Details(int id)
        {
            var service = CreateInventoryService();
            var model = service.GetFacilityInventory(id);
            return View(model);
        }
        
        // GET: Inventory/Create
        public ActionResult Create()
        {
            var itemList = new SelectList(db.Items, "ItemNumber", "Name").ToList();

            var facilityList = new SelectList(db.Facilities, "FacilityID", "Name").ToList();

            ViewBag.ItemNumber = itemList;
            ViewBag.FacilityId = facilityList;


            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInventoryService();

            if (service.CreateInventory(model))
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
            var service = CreateInventoryService();
            var model = service.Inventories().Find(e => e.InventoryId == id);
            var itemList = new SelectList(db.Items, "ItemNumber", "Name").ToList();
            var facilityList = new SelectList(db.Facilities, "FacilityID", "Name").ToList();
            var editor =
                new InventoryEdit
                {
                    InventoryId = model.InventoryId,
                    FacilityId = model.FacilityId,
                    ItemNumber = model.ItemNumber,
                    Quantity = model.Quantity,
                    Price = model.Price
                };
            ViewBag.ItemNumber = itemList;
            ViewBag.FacilityId = facilityList;

            return View(editor);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInventoryService();

            if (service.EditInventory(model))
            {
                TempData["SaveResult"] = "Inventory was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Inventory not created.");

            return View(model);
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, InventoryDetail model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInventoryService();

            if (service.DeleteInventory(id))
            {
                TempData["SaveResult"] = "Inventory was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Inventory not created.");

            return View(model);
        }
    }
}
