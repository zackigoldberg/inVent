using inVent.Models.SaleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Contracts
{
    public interface ISale
    {
        bool CreateSale(SaleCreate model);
        IEnumerable<SaleListItem> GetSales();
        SaleDetail GetSaleById(int saleId);
        bool UpdateSale(SaleEdit model);
        bool DeleteSale(int saleId);
        SaleCreate AvailableInventory();
    }
}
