﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Data
{
    public class Sale
    {
        [key]
        public int SaleId { get; set; }
        public Guid UserId { get; set; }
        public string Salesman { get; set; }
        public decimal SaleTotal { get; set; }
        public Item SoldItem { get; set; }
        public int QuantitySold { get; set; }
    }
}
