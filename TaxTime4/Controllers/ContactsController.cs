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
    public class ContactsController : Controller
    {
        private readonly TaxTime4Context _context;

        public ContactsController(TaxTime4Context context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var taxTime4Context = _context.Contact.Include(c => c.Cust);
            return View(await taxTime4Context.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Cust)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create(int custId)
        {
            Contact pass = new Contact { CustId = custId };
            pass.LastUpdated = DateTime.Now;
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId");
            return View(pass);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Contact NewContact = new Contact();
                NewContact.ContactId = contact.ContactId;
                NewContact.CustId = contact.CustId;
                NewContact.Home1 = contact.Home1;
                NewContact.Home2 = contact.Home2;
                NewContact.Home3 = contact.Home3;
                NewContact.Cell1 = contact.Cell1;
                NewContact.Cell2 = contact.Cell2;
                NewContact.Cell3 = contact.Cell3;
                NewContact.Work1 = contact.Work1;
                NewContact.Work2 = contact.Work2;
                NewContact.Work3 = contact.Work3;
                NewContact.Email = contact.Email;
                NewContact.LastUpdated = contact.LastUpdated = DateTime.Now;
                _context.Contact.Add(NewContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", contact.CustId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", contact.CustId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contact EditContact = new Contact();
                    EditContact.ContactId = contact.ContactId;
                    EditContact.CustId = contact.CustId;
                    EditContact.Home1 = contact.Home1;
                    EditContact.Home2 = contact.Home2;
                    EditContact.Home3 = contact.Home3;
                    EditContact.Cell1 = contact.Cell1;
                    EditContact.Cell2 = contact.Cell2;
                    EditContact.Cell3 = contact.Cell3;
                    EditContact.Work1 = contact.Work1;
                    EditContact.Work2 = contact.Work2;
                    EditContact.Work3 = contact.Work3;
                    EditContact.Email = contact.Email;
                    EditContact.LastUpdated = contact.LastUpdated = DateTime.Now;
                    _context.Contact.Update(EditContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", contact.CustId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Cust)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
