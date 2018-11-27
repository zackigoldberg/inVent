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
            var entity =
                new Facility()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Type = model.Type,
                    Opened = DateTimeOffset.Now,
                    FacilityId = model.FacilityId
                };
            using (var ctx = new ApplicationDbContext()){
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
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new FacilityListItem
                        {
                            FacilityId = e.FacilityId,
                            Name = e.Name,
                            Type = e.Type,
                            Items = e.Items,
                            Sales = e.Sales,
                            Opened = e.Opened,
                            Closed = e.Closed
                        }
                            );
                return query.ToArray();
            }
        }
    }
}
