using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxTime4.Models;
using TaxTime4.ViewModels;

namespace TaxTime4.Controllers
{
    public class AddressesController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;

        public AddressesController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Addresses
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            
            var taxTime4Context = _context.Address.Include(a => a.Cust);
            return View(await taxTime4Context.ToListAsync());
        }

        // GET: Addresses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: AdminDetails
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        [Authorize]
        public IActionResult Create(int custId, Customer customer, Address address)
        {
            Address pass = new Address { CustId = custId };
            
            pass.LastUpdated = DateTime.Now;
            
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId");
            return View(pass);
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,CustId,Address1,Address2,City,State,ZipCode,LastUpdated")] Address address)
        {
            if (ModelState.IsValid)
            {
                Address NewAddress = new Address();
                NewAddress.AddressId = address.AddressId;
                NewAddress.CustId = address.CustId;
                NewAddress.Address1 = address.Address1;
                NewAddress.Address2 = address.Address2;
                NewAddress.City = address.City;
                NewAddress.State = address.State;
                NewAddress.ZipCode = address.ZipCode;
                NewAddress.LastUpdated = address.LastUpdated = DateTime.Now;
                _context.Address.Add(NewAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Contacts", new { CustId = NewAddress.CustId });
            }

            //ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", address.CustId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", address.CustId);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Address EditAddress = new Address();
                    EditAddress.AddressId = address.AddressId;
                    EditAddress.CustId = address.CustId;
                    EditAddress.Address1 = address.Address1;
                    EditAddress.Address2 = address.Address2;
                    EditAddress.City = address.City;
                    EditAddress.State = address.State;
                    EditAddress.ZipCode = address.ZipCode;
                    EditAddress.LastUpdated = address.LastUpdated = DateTime.Now;
                    _context.Address.Update(EditAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Customers");
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", address.CustId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Address.FindAsync(id);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList", "Customers");
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }
    }
}
