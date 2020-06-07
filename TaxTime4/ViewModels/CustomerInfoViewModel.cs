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
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Dependent Dependent { get; set; }
        public Deposit Deposit { get; set; }
        public Appointment Appointment { get; set; }

       


        public CustomerInfoViewModel()
        {
        }
    }
}
