using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Data
{
    public class Sale
    {
        public Guid UserId { get; set; }
        public int SaleId { get; set; }
        public decimal Total { get; set; }
        public Item SoldItem { get; set; }
        public int QuantitySold { get; set; }
    }
}
