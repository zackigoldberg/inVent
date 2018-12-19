using inVent.Models.ItemModels;
using inVent.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace inVent.WebApi.Controllers
{
    [Authorize]
    public class ItemController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            ItemService itemService = CreateItemService();
            var items = itemService.GetItems();
            return Ok(items);
        }

        public IHttpActionResult Get(int id)
        {
            ItemService itemService = CreateItemService();
            var item = itemService.GetItemByNumber(id);
            return Ok(item);
        }

        public IHttpActionResult Post(ItemCreate newItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                ItemService itemService = CreateItemService();

            if (!itemService.CreateItem(newItem))
                return InternalServerError();
            return Ok();
        }
        
        public IHttpActionResult Put(ItemEdit editItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ItemService itemService = CreateItemService();

            if (!itemService.UpdateItem(editItem))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            
            ItemService itemService = CreateItemService();

            if (!itemService.DeleteItem(id))
                return InternalServerError();

            return Ok();
        }
    

        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var admin = User.IsInRole("Admin");
            var manager = User.IsInRole("Sales Manager");
            var ItemService = new ItemService(userId, admin, manager);
            return ItemService;
        }
    }
}
