﻿using inVent.Models.InventoryModels;
using inVent.Models.SaleModels;
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
    public class SaleController : Controller
    {

        // GET: Sale
        public ActionResult Index()
        {
            var service = CreateSaleService();
            var model = service.GetSales();
            return View(model);
        }

        // GET: Sale/SalesDetails/5
        [ActionName("SalesDetails")]
        public ActionResult SalesDetails(int id)
        {
            var service = CreateSaleService();
            var model = service.GetSaleById(id);
            return View(model);
        }

        // GET: Sale/Create

        public ActionResult Create()
        {
            var service = CreateSaleService();
            var itemList = new SelectList(service.Items(), "ItemNumber", "Name");
            var facilityList = new SelectList(service.Facilities(), "FacilityID", "Name");

            ViewBag.ItemNumber = itemList;
            ViewBag.FacilityId = facilityList;
            var model = service.AvailableInventory();
            model.Inventories.Sort((x, y) => string.Compare(x.Facility.Name, y.Facility.Name));
            return View(model);
        }





        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaleCreate model)
        {
            if (!ModelState.IsValid)
            {
                TempData["SaveResult"] = "Sale could not be created: sale exceeds available inventory, please try again.";
                return RedirectToAction("Index");
            }
            var service = CreateSaleService();
            var itemList = new SelectList(service.Items(), "ItemNumber", "Name");
            var facilityList = new SelectList(service.Facilities(), "FacilityID", "Name");

            ViewBag.ItemNumber = itemList;
            ViewBag.FacilityId = facilityList;
            
            var entity = service.Inventories().FirstOrDefault(e => e.FacilityId == model.FacilityId && e.ItemNumber == model.ItemNumber);
            if (entity != null)
            {
                model.InventoryId = entity.InventoryId;
            }
            else
            {
                TempData["SaveResult"] = $"The Item you are looking for is unavailable at the chosen facility";
                return View(model);
            }

            if (service.CreateSale(model)==true)
            {
                TempData["SaveResult"] = $"Sale created, the total was ${model.SaleTotal}";
                return RedirectToAction("Index");
            }
            else if (service.CreateSale(model)==false)
            {
                TempData["SaveResult"] = $"Cannot sell more items than are available please try again, current stock available at this facility:{entity.Quantity}.";
                return View(model);
            }

            ModelState.AddModelError("", "Sale failed.");

            return View(model);
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateSaleService();
            var detail = service.GetSaleById(id);
            var model =
                new SaleEdit
                {
                    SaleId =detail.SaleId,
                    FacilityId = detail.FacilityId,
                    Salesman = detail.Salesman,
                    QuantitySold = detail.QuantitySold,
                    InventoryId = detail.InventoryId,
                    Facilities = service.Facilities(),
                    Items = service.Items(),
                    SaleTotal = detail.SaleTotal,
                    ItemNumber = detail.ItemNumber
                };
            return View(model);
        }

        // POST: Sale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SaleEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSaleService();
            var entity = service.Inventories().FirstOrDefault(e => e.FacilityId == model.FacilityId && e.ItemNumber == model.ItemNumber);
            if (model.SaleId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            if (entity != null)
            {
                model.InventoryId = entity.InventoryId;
            }
            else
            {
                TempData["SaveResult"] = $"The Item you are looking for is unavailable at the chosen facility";
                ListReturn(id, model, service);
                return View(model);
            }

            if (service.UpdateSale(model) == true)
            {
                var saleTotal = service.GetSaleById(id).SaleTotal;
                TempData["SaveResult"] = $"Sale updated successfully, new total is ${saleTotal}.";
                return RedirectToAction("Index");
            }
            else if (service.UpdateSale(model) == false)
            {
                TempData["SaveResult"] = $"Cannot sell more items than are available please try again, current stock available at this facility:{entity.Quantity}.";
                ListReturn(id, model, service);
                return View(model);

            }
            ListReturn(id, model, service);
            ModelState.AddModelError("", "Sale could not be updated.");
            return View(model);
        }

        private static void ListReturn(int id, SaleEdit model, SaleService service)
        {
            model.Facilities = service.Facilities();
            model.Items = service.Items();
            model.ItemNumber = service.Inventories().SingleOrDefault(e => e.InventoryId == id).ItemNumber;
            model.FacilityId = service.Inventories().SingleOrDefault(e => e.InventoryId == id).FacilityId;
        }


        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            var service = CreateSaleService();
            var model = service.GetSaleById(id);
            return View(model);
        }

        // POST: Sale/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult SaleDelete(int id, SaleDetail model)
        {
                        var service = CreateSaleService();

            if (service.DeleteSale(id))
            {
                TempData["SaveResult"] = "Sale Deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sale could not be deleted.");
            return View(model);
        }
        private SaleService CreateSaleService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var roleId = User.IsInRole("Admin");
            var manager = User.IsInRole("Sales Manager");
            var service = new SaleService(userId, roleId, manager);
            return service;
        }
    }
}
