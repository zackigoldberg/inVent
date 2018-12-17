using inVent.Contracts;
using inVent.Data;
using inVent.Models.InventoryModels;
using inVent.Web.Controllers;
using inVent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Services 
{
    public class InventoryService : IInventory
    {
        private readonly Guid _userId;
        private readonly bool _adminId;

        public InventoryService(Guid userId,bool adminId)
        {
            _userId = userId;
            _adminId = adminId;
        }

        public bool CreateInventory(InventoryCreate model)
        {
            var entity = new Inventory();

            entity.FacilityId = model.FacilityId;
            entity.ItemNumber = model.ItemNumber;
            entity.Quantity = model.Quantity;
            entity.Price = model.Price;
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Inventories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InventoryListItem> GetAllInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Inventories
                    .Select(
                        e =>
                        new InventoryListItem
                        {
                            InventoryId = e.InventoryId,
                            FacilityId = e.FacilityId,
                            Facility = e.Facility,
                            ItemNumber = e.ItemNumber,
                            Item = e.Item,
                            Quantity = e.Quantity,
                            Price = e.Price
                        }
                        );
                return query.ToArray();
            }
        }
        public InventoryDetail GetInventoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Inventories
                    .Single(e => e.InventoryId == id);
                return
                    new InventoryDetail
                    {
                        FacilityId = entity.FacilityId,
                        Facility = entity.Facility,
                        ItemNumber = entity.ItemNumber,
                        Item = entity.Item,
                        InventoryId = entity.InventoryId,
                        Quantity = entity.Quantity,
                        Price = entity.Price
                    };
            }
        }
        public IEnumerable<InventoryAdvancedListItem> GetFacilityInventory(int facilityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Dictionary<int, int> itemNumbers = new Dictionary<int, int>();
                List<InventoryAdvancedListItem> updateList = new List<InventoryAdvancedListItem>();
                foreach (Inventory i in ctx.Inventories)
                {
                    if (i.FacilityId == facilityId)
                    {
                        if (!itemNumbers.ContainsKey(i.ItemNumber))
                        {
                            itemNumbers.Add(i.ItemNumber, i.Quantity);
                        }
                    }
                }
                foreach (var kvp in itemNumbers)
                {
                    var query = ctx
                            .Items
                            .Where(e => e.ItemNumber == kvp.Key)
                            .Select(
                            e =>
                            new InventoryAdvancedListItem
                            {
                                FacilityId = facilityId,
                                ItemId = kvp.Key,
                                ItemName = e.Name,
                                Description = e.Description,
                                Quantity = kvp.Value
                            }
                            );
                    updateList.Add(query.FirstOrDefault());
                }
                return updateList.ToArray();
            }
        }

        public bool EditInventory(InventoryEdit model)
        {
            if (_adminId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Inventories
                        .Single(e => e.InventoryId == model.InventoryId);

                    entity.FacilityId = model.FacilityId;
                    entity.ItemNumber = model.ItemNumber;
                    entity.Quantity = model.Quantity;
                    entity.Price = model.Price;

                    return ctx.SaveChanges() == 1;
                }
            }
            return false;
        }

        public bool DeleteInventory(int inventoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Inventories
                    .Single(e => e.InventoryId == inventoryId && _adminId);
                ctx.Inventories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public List<Facility> Facilities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Facilities.ToList();
            }
        }
        public List<Item> Items()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Items.ToList();
            }
        }
        public List<Inventory> Inventories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Inventories.ToList();
            }
        }
    }
}
