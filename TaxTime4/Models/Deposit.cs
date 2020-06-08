using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxTime4.Models
{
    public partial class Deposit
    {
        [ForeignKey("CustId")]
        public int CustId { get; set; }
        public double? Routing { get; set; }
        public double? Account { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }
    }
}
