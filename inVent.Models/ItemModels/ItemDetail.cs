using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.ItemModels
{
    public class ItemDetail
    {
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Pack Size:")]
        public string PackSize { get; set; }
    }
}
