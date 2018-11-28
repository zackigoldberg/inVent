﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.InventoryModels
{
    public class InventoryAdvancedListItem
    {
        public int FacilityId { get; set; }
        public object ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public object Quantity { get; set; }
    }
}
