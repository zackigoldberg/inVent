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
        [Display(Name = "What Facility will the new inventory be tied to?")]
        public int FacilityId { get; set; }
        [Display(Name = "Which item would you like to tie to this facility?")]
        public int ItemId { get; set; }
        [Display(Name = "How many items are there?")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
