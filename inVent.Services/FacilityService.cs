using inVent.Data;
using inVent.Models.FacilityModels;
using inVent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Services
{
    public class FacilityService
    {
        private readonly Guid _userId;

        public FacilityService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFacility(FacilityCreate model)
        {
            List<Item> itemList = new List<Item>();
            //returns the current master list of items that could be at the facility
            using (var ctx = new ApplicationDbContext())
            {
                itemList = ctx.Items.ToList();
            }
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
                    .Single(e => e.FacilityId == model.FacilityId && e.OwnerId == _userId);
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
                    .Single(e => e.FacilityId == FacilityId && e.OwnerId == _userId);
                    entity.Closed = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
