﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.SaleModels
{
    public class SaleCreate
    {
        public string Salesman { get; set; }
        public int InventoryId { get; set; }
        public int QuantitySold { get; set; }
        public decimal SaleTotal { get; set; }
    }
}
