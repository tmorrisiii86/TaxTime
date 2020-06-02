using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxTime4.Models
{
    public partial class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [ForeignKey("CustId")]
        public int? CustId { get; set; }
        public decimal? Home1 { get; set; }
        public decimal? Home2 { get; set; }
        public decimal? Home3 { get; set; }
        public decimal? Cell1 { get; set; }
        public decimal? Cell2 { get; set; }
        public decimal? Cell3 { get; set; }
        public decimal? Work1 { get; set; }
        public decimal? Work2 { get; set; }
        public decimal? Work3 { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }
    }
}
