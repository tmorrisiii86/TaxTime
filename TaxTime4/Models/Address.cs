using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxTime4.Models;

namespace TaxTime4.Models
{
    public partial class Address
    {
        [Key]
        public int AddressId { get; set; }
        [ForeignKey("CustId")]
        public int CustId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? ZipCode { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }

    }

}
