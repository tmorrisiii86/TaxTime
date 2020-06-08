using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxTime4.Models;
using System.Text.RegularExpressions;

namespace TaxTime4.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Address = new HashSet<Address>();
            Contact = new HashSet<Contact>();
            Dependent = new HashSet<Dependent>();
        }
        [Key] [ForeignKey("CustId")]
        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Ssn1 { get; set; }
        public int? Ssn2 { get; set; }
        public int? Ssn3 { get; set; }
        public DateTime LastUpdated { get; set; }

        

        public Appointment Appointment { get; set; }
        public Deposit Deposit { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Dependent> Dependent { get; set; }


    }
}
