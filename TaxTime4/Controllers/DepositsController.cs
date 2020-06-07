using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxTime4.Models;

namespace TaxTime4.Controllers
{
    public class DepositsController : Controller
    {
        private readonly TaxTime4Context _context;

        public DepositsController(TaxTime4Context context)
        {
            _context = context;
        }

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            var taxTime4Context = _context.Deposit.Include(d => d.Cust);
            return View(await taxTime4Context.ToListAsync());
        }

        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit
                .Include(d => d.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // GET: Deposits/Create
        public IActionResult Create(int custId)
        {
            Deposit pass = new Deposit { CustId = custId };
            pass.LastUpdated = DateTime.Now;
            
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId");
            return View(pass);
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                Deposit NewDeposit = new Deposit();
                NewDeposit.CustId = deposit.CustId;
                NewDeposit.Routing = deposit.Routing;
                NewDeposit.Account = deposit.Account;
                NewDeposit.LastUpdated = deposit.LastUpdated = DateTime.Now;
                _context.Deposit.Add(NewDeposit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Appointments", new { CustId = NewDeposit.CustId });
            }

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", deposit.CustId);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", deposit.CustId);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Deposit deposit)
        {
            if (id != deposit.CustId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Deposit EditDeposit = new Deposit();
                    EditDeposit.CustId = deposit.CustId;
                    EditDeposit.Routing = deposit.Routing;
                    EditDeposit.Account = deposit.Account;
                    EditDeposit.LastUpdated = deposit.LastUpdated = DateTime.Now;
                    _context.Deposit.Update(EditDeposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositExists(deposit.CustId))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", deposit.CustId);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposit
                .Include(d => d.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposit.FindAsync(id);
            _context.Deposit.Remove(deposit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositExists(int id)
        {
            return _context.Deposit.Any(e => e.CustId == id);
        }
    }
}
