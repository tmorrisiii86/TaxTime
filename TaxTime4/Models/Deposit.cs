using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxTime4.Models
{
    public partial class Deposit
    {
        [Key]
        public int CustId { get; set; }
        public decimal? Routing { get; set; }
        public decimal? Account { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }
    }
}
