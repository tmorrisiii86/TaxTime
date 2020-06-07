using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxTime4.Models;

namespace TaxTime4.Models
{
    public partial class Appointment
    {
        [Key]
        public int CustId { get; set; }
        public DateTime? LastAppt { get; set; }
        public DateTime? NextAppt { get; set; }
        public DateTime LastUpdated { get; set; }

        public Customer Cust { get; set; }

        public Contact Contact { get; set; }
        public Customer Customer { get; set; }

    }
}
