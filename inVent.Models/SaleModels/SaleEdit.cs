using inVent.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.SaleModels
{
    public class SaleEdit
    {
        [Display(Name = "Sale Id")]
        public int SaleId { get; set; }
        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }
        [Display(Name = "Item Number")]
        public int ItemNumber { get; set; }
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        [Display(Name = "Quantity Sold")]
        public int QuantitySold { get; set; }
        [Display(Name = "Sale Total")]
        public decimal SaleTotal { get; set; }
        public string Salesman { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<Item> Items { get; set; }
       
    }
}
