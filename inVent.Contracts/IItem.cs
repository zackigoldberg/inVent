using inVent.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Contracts
{
    public interface IItem
    {
        bool CreateItem(ItemCreate model);
        IEnumerable<ItemListItem> GetItems();
        ItemDetail GetItemByNumber(int itemNumber);
        bool UpdateItem(ItemEdit model);
        bool DeleteItem(int itemNumber);
    }
}
