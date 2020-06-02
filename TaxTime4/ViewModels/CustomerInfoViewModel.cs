using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaxTime4.Models;

namespace TaxTime4.ViewModels
{
    public class CustomerInfoViewModel
    {
        public Customer customer { get; set; }
        public Address address { get; set; }
        public Contact contact { get; set; }
        public Dependent dependent { get; set; }
        public Deposit deposit { get; set; }
        public Appointment appointment { get; set; }
        

        public CustomerInfoViewModel()
        {
        }
    }
}
