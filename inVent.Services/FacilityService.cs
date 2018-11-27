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

        public bool CreateFacility()
        {

        }

        public IEnumerable<FacilityListItem> GetFacilities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = 
                    ctx
                    .Facilities
                    .Where(e=> e.OwnerId == _userId)
                    .Select(
                        e=> 
                        new )
            }
        }
    }
}
