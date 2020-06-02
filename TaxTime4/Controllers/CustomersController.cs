﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CustomersController(TaxTime4Context context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {

            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer, Address address)
        {
            if (ModelState.IsValid)
            {
                Customer NewCustomer = new Customer();
                NewCustomer.CustId = customer.CustId;
                NewCustomer.FirstName = customer.FirstName;
                NewCustomer.LastName = customer.LastName;
                NewCustomer.Ssn1 = customer.Ssn1;
                NewCustomer.Ssn2 = customer.Ssn2;
                NewCustomer.Ssn3 = customer.Ssn3;
                NewCustomer.LastUpdated = customer.LastUpdated = DateTime.Now;
                _context.Customer.Add(NewCustomer);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Create", "Addresses", new { id = customer.CustId == address.CustId });
            }


            return View(customer);
        }

        // GET: Customers/Edit/5
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustId == id);
        }
    }
}
