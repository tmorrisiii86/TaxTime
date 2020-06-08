using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaxTime4.Models;
using TaxTime4.ViewModels;


namespace TaxTime4.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;


        public CustomersController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        

        // GET: Customers
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var customers = from c in _context.Customer select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString));
            }
            return View(await customers.ToListAsync());
        }

        // GET: Admin Customer List Index
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminList(string searchString)
        {
            var customers = from c in _context.Customer select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString));
            }
            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: AllCustomerInfo
        [Authorize]
        public async Task<IActionResult> AllCustomerInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var allcustomer = (from Customer in _context.Customer
                               join Address in _context.Address on Customer.CustId equals Address.CustId
                               join Contact in _context.Contact on Customer.CustId equals Contact.CustId
                               join Dependent in _context.Dependent on Customer.CustId equals Dependent.CustId
                               join Deposit in _context.Deposit on Customer.CustId equals Deposit.CustId
                               join Appointment in _context.Appointment on Customer.CustId equals Appointment.CustId
                               select new { Customer, Address, Contact, Dependent, Deposit, Appointment })
                               .Where(c => c.CustId == id); */

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.All = (_context.Customer, _context.Address, _context.Contact, _context.Dependent, _context.Deposit, _context.Appointment);
            return View(customer);
        }

        // GET: AdminDetails
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.All = (_context.Customer, _context.Address, _context.Contact, _context.Dependent, _context.Deposit, _context.Appointment);
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public IActionResult Create(int custId)
        {
            Customer pass = new Customer { CustId = custId };
            pass.LastUpdated = DateTime.Now;
            return View(pass);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustId,FirstName,LastName,Ssn1,Ssn2,Ssn3,LastUpdated")] Customer customer, Address address)
        {
            if (ModelState.IsValid)
            {
                Customer NewCustomer = new Customer();
                //NewCustomer.CustId = customer.CustId;
                NewCustomer.FirstName = customer.FirstName;
                NewCustomer.LastName = customer.LastName;
                NewCustomer.Ssn1 = customer.Ssn1;
                NewCustomer.Ssn2 = customer.Ssn2;
                NewCustomer.Ssn3 = customer.Ssn3;
                NewCustomer.LastUpdated = customer.LastUpdated = DateTime.Now;
                _context.Customer.Add(NewCustomer);
                await _context.SaveChangesAsync();
                NewCustomer.CustId = _context.Customer.Max(c => c.CustId);
                return RedirectToAction("Create", "Addresses", new { CustId = NewCustomer.CustId });
            }


            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, int custId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            Customer pass = new Customer { CustId = custId };
            pass.LastUpdated = DateTime.Now;
            
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Customer EditCustomer = new Customer();
                    EditCustomer.CustId = customer.CustId;
                    EditCustomer.FirstName = customer.FirstName;
                    EditCustomer.LastName = customer.LastName;
                    EditCustomer.Ssn1 = customer.Ssn1;
                    EditCustomer.Ssn2 = customer.Ssn2;
                    EditCustomer.Ssn3 = customer.Ssn3;
                    EditCustomer.LastUpdated = customer.LastUpdated = DateTime.Now;
                    _context.Customer.Update(EditCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            var address = await _context.Address.FindAsync(id);
            var contact = await _context.Contact.FindAsync(id);
            var dependent = await _context.Dependent.FindAsync(id);
            var deposit = await _context.Deposit.FindAsync(id);
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Customer.Remove(customer);
            _context.Address.Remove(address);
            _context.Contact.Remove(contact);
            _context.Dependent.Remove(dependent);
            _context.Deposit.Remove(deposit);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminList));
        } 

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustId == id);
        }
    }
}
