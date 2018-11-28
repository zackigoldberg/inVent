using inVent.Models.ItemModels;
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
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            var service = CreateItemService();
            var model = service.GetItems();
            return View(model);
        }

        //Get: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateItemService();

            if (service.CreateItem(model))
            {
                TempData["Save Result"] = "Your item was created!";
            }

            ModelState.AddModelError("", "Item was not created.");

            return View(model);
        }

        //Get: Item/Display
        public ActionResult Display(int itemNumber)
        {
            var service = CreateItemService();
            var model = service.GetItemByNumber(itemNumber);
            return View(model);
        }

        //Get: Item/Edit
        public ActionResult Edit(int itemNumber)
        {
            var service = CreateItemService();
            var detail = service.GetItemByNumber(itemNumber);
            var model =
                new ItemEdit
                {
                    ItemNumber = detail.ItemNumber,
                    Name = detail.Name,
                    Description = detail.Description,
                    Price = detail.Price,
                    Stock = detail.Stock
                };
            return View(model);
        }

        //Post: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ItemNumber != id)
            {
                ModelState.AddModelError("", "Item numbers did not atch");
                return View(model);
            }
            var service = CreateItemService();

            if (service.UpdateItem(model))
            {
                TempData["Save Result"] = "Item was Updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Item was not updated.");

            return View(model);
        }

        //Get: Item/Delete
        public ActionResult Delete(int itemNumber)
        {
            var service = CreateItemService();
            var model = service.GetItemByNumber(itemNumber);
            return View(model);
        }

        //Post: Item/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubmit(int id)
        {
            var service = CreateItemService();

            service.DeleteItem(id);

            TempData["Save Result"] = "Item was deleted successfully!";

            return RedirectToAction("Index");
        }
        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemService(userId);
            return service;
        }
    }
}