using inVent.Data;
using inVent.Models.FacilityModels;
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

        public SaleService(Guid userid)
        {
            _userId = userid;
        }


        public bool CreateSale(SaleCreate model)
        {
            List<Inventory> saleList = new List<Inventory>();
            var entity = new Sale();


            entity.UserId = _userId;
            entity.InventoryId = model.InventoryId;
            entity.Salesman = model.Salesman;
            entity.QuantitySold = model.QuantitySold;
            entity.SaleTotal = model.SaleTotal;
               
            entity = GetSaleTotal(entity);
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Sales.Add(entity);
                return ctx.SaveChanges() == 1;
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
                            Salesman = e.Salesman,
                            //FacilityId = e.Facility.FacilityId,
                            //Facility = e.Facility,
                            //ItemNumber = e.Item.ItemNumber,
                            //Item = e.Item,
                            InventoryId = e.Inventory.InventoryId,
                            //Inventory = e.Inventory,
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
                        Salesman = entity.Salesman,
                        //FacilityId = entity.Facility.FacilityId,
                        //Facility = entity.Facility,
                        //ItemNumber = entity.Item.ItemNumber,
                        //Item = entity.Item,
                        InventoryId = entity.Inventory.InventoryId,
                        //Inventory = entity.Inventory,
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
            using( var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Sales
                    .Single(e => e.SaleId == model.SaleId);

                entity.Salesman = model.Salesman;
                entity.Inventory = ctx.Inventories.Single(e => e.InventoryId == model.InventoryId);
                entity.QuantitySold = model.QuantitySold;
                entity.SaleTotal = model.SaleTotal;

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
                    .Single(e => e.SaleId == saleId);
                ctx.Sales.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        private Sale GetSaleTotal(Sale model)
        {
            model.SaleTotal = model.Inventory.Price * model.QuantitySold;

            return model;
        }
    }

}

