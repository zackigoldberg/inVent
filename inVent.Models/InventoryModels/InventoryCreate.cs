using inVent.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.InventoryModels
{
    public class InventoryCreate
    {
        [Required]
        [Display(Name = "What Facility will the new inventory be tied to?")]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
        public List<Item> FacilityList { get; set; }

        [Required]
        [Display(Name = "Which item would you like to tie to this facility?")]
        public int ItemNumber { get; set; }
        public Item Item { get; set; }
        public List<Item> ItemList { get; set; }

        [Required]
        [Display(Name = "How many items are there?")]
        [Range(0,9999999, ErrorMessage ="Please enter a non-negative number.")]
        public int Quantity { get; set; }


        [Required]
        [Range(0, 9999999, ErrorMessage = "Please enter a non-negative number.")]
        public decimal Price { get; set; }
    }
}
