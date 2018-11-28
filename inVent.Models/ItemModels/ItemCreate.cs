using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.ItemModels
{
    public class ItemCreate
    {
        [Required]
        [MinLength(3, ErrorMessage ="Please enter at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Emon Lusk does not like names of this length.")]
            public string Name { get; set; }

        [MaxLength(240, ErrorMessage = "Keep it short and sweet")]
        public string Description { get; set; }
        
        [Range(0,999999, ErrorMessage = "Items must have a non-negative value")]
        public decimal Price { get; set; }

        [Range(0, 9999999, ErrorMessage = "Invalid stock range, cannot go to negative stock.")]
        public int Stock { get; set; }
    }
}
