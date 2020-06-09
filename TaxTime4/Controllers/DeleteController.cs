using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxTime4.Models;
using TaxTime4.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TaxTime4.Controllers
{
    public class DeleteController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;

        public DeleteController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAll(int? id)
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

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            var address = await _context.Address.FindAsync(id);
            _context.Address.Remove(address);
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            var dependent = await _context.Dependent.FindAsync(id);
            _context.Dependent.Remove(dependent);
            var deposit = await _context.Deposit.FindAsync(id);
            _context.Deposit.Remove(deposit);
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList", "Customers");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustId == id);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}