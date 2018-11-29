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
            List<Sale> saleList = new List<Sale>();

            using (var ctx = new ApplicationDbContext())
            {
                saleList = ctx.Sales.ToList();
            }
            var entity =
                new Sale()
                {
                    UserId = _userId,
                    Salesman = model.Salesman,
                    FacilityId = model.FacilityId,
                    ItemNumber = model.ItemNumber,
                    InventoryId = model.InventoryId,
                    QuantitySold = model.QuantitySold,
                    SaleTotal = model.SaleTotal
                };
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
                            FacilityId = e.FacilityId,
                            Facility = e.Facility,
                            ItemNumber = e.ItemNumber,
                            Item = e.Item,
                            InventoryId = e.InventoryId,
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
                        Salesman = entity.Salesman,
                        FacilityId = entity.FacilityId,
                        Facility = entity.Facility,
                        ItemNumber = entity.ItemNumber,
                        Item = entity.Item,
                        InventoryId = entity.InventoryId,
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
                entity.FacilityId = model.FacilityId;
                entity.ItemNumber = model.ItemNumber;
                entity.InventoryId = model.InventoryId;
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

