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
        
        public Guid UserId { get; set; }
        [Key]
        public int ItemNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Pack Size:")]
        public string PackSize { get; set; }
    }
}
