using inVent.Models.InventoryModels;
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
        [ActionName("Details")]
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
            if (!ModelState.IsValid) return View(model);

            var service = CreateSaleService();

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
                    FacilityId = detail.FacilityId,
                    Salesman = detail.Salesman,
                    QuantitySold = detail.QuantitySold,
                    InventoryId = detail.InventoryId,
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

            if (model.SaleId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }


            var service = CreateSaleService();

            if (service.UpdateSale(model))
            {
                TempData["SaveResult"] = "Sale was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sale could not be updated.");
            return View(model);
        }


        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sale/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SaleDetail model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SaleId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }


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
            var service = new SaleService(Guid.Parse(User.Identity.GetUserId()));
            return service;
        }
    }
}
