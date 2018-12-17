using inVent.Models.InventoryModels;
using inVent.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Contracts
{
   public interface IInventory
    {
        bool CreateInventory(InventoryCreate model);
        IEnumerable<InventoryListItem> GetAllInventory();
        InventoryDetail GetInventoryById(int id);
        IEnumerable<InventoryAdvancedListItem> GetFacilityInventory(int facilityId);
        bool EditInventory(InventoryEdit model);
        bool DeleteInventory(int inventoryId);
    }
}
