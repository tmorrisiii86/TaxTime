using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxTime4.Models
{
    public partial class Dependent
    {
        [Key]
        public int DepId { get; set; }
        public int? CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Ssn1 { get; set; }
        public  int? Ssn2 { get; set; }
        public int? Ssn3 { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool CheckBox { get; set; }

        public Customer Cust { get; set; }
    }
}
