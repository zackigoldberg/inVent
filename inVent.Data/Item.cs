using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Data
{
    public class Item
    {
        [Key]
        public Guid UserId { get; set; }
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public Item(int additionalStock)
        {
            Stock += additionalStock;
        }

        public Item()
        {

        }
    }
}
