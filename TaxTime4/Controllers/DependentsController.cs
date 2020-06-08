using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxTime4.Models;

namespace TaxTime4.Controllers
{
    public class DependentsController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;

        public DependentsController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dependents
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            if (id == null)
            {
                NotFound();
            }
            var dependents = _context.Dependent.Include(a => a.Cust)
                .Where(d => d.CustId == id)
                .ToList();

            if(dependents == null)
            {
                NotFound();
            }

            return View(dependents);
        }

        // GET: Dependents/AdminDetails
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDetails(int id)
        {
            if (id == null)
            {
                NotFound();
            }
            var dependents = _context.Dependent.Include(a => a.Cust)
                .Where(d => d.CustId == id)
                .ToList();

            if (dependents == null)
            {
                NotFound();
            }

            return View(dependents);
        }

        // GET: Dependents/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .Include(d => d.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // GET: Dependents/Create
        [Authorize]
        public IActionResult Create(int custId)
        {
            Dependent pass = new Dependent { CustId = custId };
            pass.LastUpdated = DateTime.Now;

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId");
            return View(pass);
        }

        // POST: Dependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                Dependent NewDependent = new Dependent();
                NewDependent.DepId = dependent.DepId;
                NewDependent.CustId = dependent.CustId;
                NewDependent.FirstName = dependent.FirstName;
                NewDependent.LastName = dependent.LastName;
                NewDependent.Ssn1 = dependent.Ssn1;
                NewDependent.Ssn2 = dependent.Ssn2;
                NewDependent.Ssn3 = dependent.Ssn3;
                NewDependent.LastUpdated = dependent.LastUpdated = DateTime.Now;
                NewDependent.CheckBox = dependent.CheckBox;
                _context.Dependent.Add(NewDependent);
                await _context.SaveChangesAsync();

                if (NewDependent.CheckBox == true)
                {
                    return RedirectToAction("Create", "Dependents", new { CustId = NewDependent.CustId });
                }
                else
                {
                    return RedirectToAction("Create", "Deposits", new { CustId = NewDependent.CustId });
                }
            }

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", dependent.CustId);
            return View(dependent);
        }

        // GET: Dependents/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", dependent.CustId);
            return View(dependent);
        }

        // POST: Dependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dependent dependent)
        {
            if (id != dependent.DepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Dependent EditDependent = new Dependent();
                    EditDependent.DepId = dependent.DepId;
                    EditDependent.CustId = dependent.CustId;
                    EditDependent.FirstName = dependent.FirstName;
                    EditDependent.LastName = dependent.LastName;
                    EditDependent.Ssn1 = dependent.Ssn1;
                    EditDependent.Ssn2 = dependent.Ssn2;
                    EditDependent.Ssn3 = dependent.Ssn3;
                    EditDependent.LastUpdated = dependent.LastUpdated = DateTime.Now;
                    _context.Dependent.Update(EditDependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependentExists(dependent.DepId))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", dependent.CustId);
            return View(dependent);
        }

        // GET: Dependents/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.Dependent
                .Include(d => d.Cust)
                .FirstOrDefaultAsync(m => m.DepId == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // POST: Dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependent = await _context.Dependent.FindAsync(id);
            _context.Dependent.Remove(dependent);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList", "Customers");
        }

        private bool DependentExists(int id)
        {
            return _context.Dependent.Any(e => e.DepId == id);
        }
    }
}
