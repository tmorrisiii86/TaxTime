using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxTime4.Models;

namespace TaxTime4.Models
{
    public partial class Appointment
    {
        [ForeignKey("CustId")]
        public int CustId { get; set; }
        public DateTime? LastAppt { get; set; }
        public DateTime? NextAppt { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }


    }
}
