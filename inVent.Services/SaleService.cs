using inVent.Data;
using inVent.Models.FacilityModels;
using inVent.Models.InventoryModels;
using inVent.Models.SaleModels;
using inVent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Services
{
    public class SaleService
    {
        private readonly Guid _userId;
        private readonly bool _adminId;

        public SaleService(Guid userId, bool adminId)
        {
            _userId = userId;
            _adminId = adminId;
        }

        public bool CreateSale(SaleCreate model)
        {
            List<Inventory> saleList = new List<Inventory>();
            var entity = new Sale();

            entity.UserId = _userId;
            entity.InventoryId = model.InventoryId;
            entity.Salesman = model.Salesman;
            entity.QuantitySold = model.QuantitySold;

            entity = GetSaleTotal(entity);
            using (var ctx = new ApplicationDbContext())
            {
                if (ctx.Inventories.Single(e => e.InventoryId == entity.InventoryId).Quantity >= entity.QuantitySold)
                {
                    ctx.Inventories.Single(e => e.InventoryId == entity.InventoryId).Quantity -= entity.QuantitySold;
                    ctx.Sales.Add(entity);
                    return ctx.SaveChanges() == 2;
                }
                return false;

            }
        }
        public IEnumerable<SaleListItem> GetSales()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Sales
                    .Select(
                        e =>
                        new SaleListItem
                        {
                            SaleId = e.SaleId,
                            Salesman = e.Salesman,
                            FacilityId = e.Inventory.Facility.FacilityId,
                            Facility = e.Inventory.Facility,
                            ItemNumber = e.Inventory.Item.ItemNumber,
                            Item = e.Inventory.Item,
                            InventoryId = e.Inventory.InventoryId,
                            Inventory = e.Inventory,
                            QuantitySold = e.QuantitySold,
                            SaleTotal = e.SaleTotal
                        }
                        );
                return query.ToArray();
            }
        }

        public SaleDetail GetSaleById(int saleId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.SaleId == saleId);
                return
                    new SaleDetail
                    {
                        SaleId = saleId,
                        Salesman = entity.Salesman,
                        FacilityId = entity.Inventory.FacilityId,
                        Facility = entity.Inventory.Facility,
                        ItemNumber = entity.Inventory.ItemNumber,
                        Item = entity.Inventory.Item,
                        InventoryId = entity.Inventory.InventoryId,
                        Inventory = entity.Inventory,
                        QuantitySold = entity.QuantitySold,
                        SaleTotal = entity.SaleTotal
                    };
            }
        }
        public SaleDetail GetSaleByFacilityId(int facilityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.Inventory.FacilityId == facilityId);
                return
                    new SaleDetail
                    {
                        SaleId = entity.SaleId,
                        Salesman = entity.Salesman,
                        FacilityId = entity.Inventory.FacilityId,
                        Facility = entity.Inventory.Facility,
                        ItemNumber = entity.Inventory.ItemNumber,
                        Item = entity.Inventory.Item,
                        InventoryId = entity.Inventory.InventoryId,
                        Inventory = entity.Inventory,
                        QuantitySold = entity.QuantitySold,
                        SaleTotal = entity.SaleTotal
                    };
            }
        }
        public SaleDetail GetSaleByInventoryId(int inventoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.Inventory.InventoryId == inventoryId);
                return
                    new SaleDetail
                    {
                        Salesman = entity.Salesman,
                        FacilityId = entity.Inventory.FacilityId,
                        Facility = entity.Inventory.Facility,
                        ItemNumber = entity.Inventory.ItemNumber,
                        Item = entity.Inventory.Item,
                        InventoryId = entity.Inventory.InventoryId,
                        Inventory = entity.Inventory,
                        QuantitySold = entity.QuantitySold,
                        SaleTotal = entity.SaleTotal
                    };
            }
        }
        public bool UpdateSale(SaleEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.SaleId == model.SaleId);

                var inventory =
                    ctx
                    .Inventories
                    .Single(e => e.InventoryId == model.InventoryId);

                entity.SaleId = model.SaleId;
                entity.Salesman = model.Salesman;
                entity.Inventory = ctx.Inventories.Single(e => e.InventoryId == model.InventoryId);
                if (entity.QuantitySold != model.QuantitySold)
                {
                    var difference = model.QuantitySold - entity.QuantitySold;
                    if (difference<= inventory.Quantity)
                    {
                        inventory.Quantity-=difference;
                        entity.QuantitySold = model.QuantitySold;
                        entity = GetSaleTotal(entity);
                        return ctx.SaveChanges() == 2;
                    }
                }
                return ctx.SaveChanges() == 1;
                
            }
        }
        public bool DeleteSale(int saleId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.SaleId == saleId)
                ctx.Sales.Remove(entity);

                ctx.Inventories.Single(e => e.InventoryId == entity.InventoryId).Quantity += entity.QuantitySold;
                return ctx.SaveChanges() == 2;
            }
        }
        private Sale GetSaleTotal(Sale model)
        {
            using (var ctx = new ApplicationDbContext()) { model.SaleTotal = (ctx.Inventories.First(e => e.InventoryId == model.InventoryId).Price * model.QuantitySold); }

            return model;
        }
        public List<Item> Items()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Items.ToList();
            }
        }
        public List<Facility> Facilities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Facilities.ToList();
            }
        }
        public List<Inventory> Inventories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Inventories.ToList();
            }
        }
        public SaleCreate AvailableInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var model =
                new SaleCreate
                {
                    Inventories = ctx.Inventories.Select(e =>
                   new InventoryListItem
                   {
                       InventoryId = e.InventoryId,
                       FacilityId = e.FacilityId,
                       Facility = e.Facility,
                       ItemNumber = e.ItemNumber,
                       Item = e.Item,
                       Quantity = e.Quantity,
                       Price = e.Price
                   }).ToList()

                };
                return model;
            }
        }
    }

}

