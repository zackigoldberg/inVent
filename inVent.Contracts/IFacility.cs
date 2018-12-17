using inVent.Models.FacilityModels;
using inVent.Models.InventoryModels;
using inVent.Models.SaleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Contracts
{
    public interface IFacility
    {
        bool CreateFacility(FacilityCreate model);
        IEnumerable<FacilityListItem> GetFacilities();
        IEnumerable<FacilityListItem> GetClosedFacilities();
        FacilityDetail GetFacilityById(int facilityId);
        bool UpdateFacility(FacilityEdit model);
        bool CloseFacility(int FacilityId);
        List<SaleListItem> SalesByFacilityId(int id);
        List<InventoryListItem> Inventories(int id);
    }
}
