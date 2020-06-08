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
        public int? Home1 { get; set; }
        public int? Home2 { get; set; }
        public int? Home3 { get; set; }
        public int? Cell1 { get; set; }
        public int? Cell2 { get; set; }
        public int? Cell3 { get; set; }
        public int? Work1 { get; set; }
        public int? Work2 { get; set; }
        public int? Work3 { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool CheckBox { get; set; }


        public Customer Cust { get; set; }
    }
}
