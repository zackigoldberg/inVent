using inVent.Contracts;
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
    public class FacilityService : IFacility
    {
        private readonly Guid _userId;
        private readonly bool _admin;
        public FacilityService(Guid userId, bool admin)
        {
            _userId = userId;
            _admin = admin;
        }
        public bool CreateFacility(FacilityCreate model)
        {
           var entity =
                new Facility()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Type = model.Type,
                    Opened = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Facilities.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FacilityListItem> GetFacilities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Facilities
                    .Where(e => e.Closed == null)
                    .Select(
                        e =>
                        new FacilityListItem
                        {
                            FacilityId = e.FacilityId,
                            Name = e.Name,
                            Type = e.Type,
                            Opened = e.Opened,
                            Closed = e.Closed
                        }
                            );
                return query.ToArray();
            }
        }

        public IEnumerable<FacilityListItem> GetClosedFacilities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Facilities
                    .Where(e => e.Closed != null)
                    .Select(
                        e =>
                        new FacilityListItem
                        {
                            FacilityId = e.FacilityId,
                            Name = e.Name,
                            Type = e.Type,
                            Opened = e.Opened,
                            Closed = e.Closed
                        }
                            );
                return query.ToArray();
            }
        }

        public FacilityDetail GetFacilityById(int facilityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =    
                    ctx
                    .Facilities
                    .Single(e => e.FacilityId == facilityId);
                return
                    new FacilityDetail
                    {
                        FacilityId = entity.FacilityId,
                        Name = entity.Name,
                        Type = entity.Type,
                        Opened = entity.Opened,
                        Closed = entity.Closed
                    };
            }
        }
        public bool UpdateFacility(FacilityEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Facilities
                    .Single(e => e.FacilityId == model.FacilityId && (e.OwnerId == _userId ||  _admin));
                entity.Name = model.Name;
                entity.Type = model.Type;

                return ctx.SaveChanges() == 1;
                            } 
        }
        public bool CloseFacility(int FacilityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Facilities
                    .Single(e => e.FacilityId == FacilityId && (e.OwnerId == _userId ||  _admin));
                entity.Closed = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }
        public List<SaleListItem> SalesByFacilityId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Sales
                    .Where(e => e.Inventory.FacilityId == id)
                    .Select(e =>
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
                    });
                return query.ToList();
            } 
        }
        public List<InventoryListItem> Inventories(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query =
                 ctx
                 .Inventories
                 .Where(e => e.FacilityId == id)
                 .Select( e=>
                 new InventoryListItem
                 {
                     InventoryId = e.InventoryId,
                     FacilityId = e.FacilityId,
                     Facility = e.Facility,
                     ItemNumber = e.ItemNumber,
                     Item = e.Item,
                     Quantity = e.Quantity,
                     Price = e.Price
                 });
                return query.ToList();
            }
        }
    }
}
