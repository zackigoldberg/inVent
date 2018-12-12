using inVent.Models.InventoryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.SaleModels
{
    public class SaleCreate
    {
        public int InventoryId { get; set; }
        public List<InventoryListItem> Inventories { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Please enter at least 3 characters.")]
        public string Salesman { get; set; }
        [Required]
        [Display(Name ="Quantity Sold")]
        [Range(0,999999999,ErrorMessage ="Please enter a non-negative quantity")]
        public int QuantitySold { get; set; }
        [Display(Name = "Sale Total")]
        public decimal SaleTotal { get; set; }
        [Display(Name = "Facility")]
        public int FacilityId { get; set; }
        [Display(Name = "Item")]
        public int ItemNumber { get; set; }
    }
}
