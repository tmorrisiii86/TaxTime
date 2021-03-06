﻿using System;
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
    public class ContactsController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;

        public ContactsController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Contacts
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var taxTime4Context = _context.Contact.Include(c => c.Cust);
            return View(await taxTime4Context.ToListAsync());
        }

        // GET: Contacts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Get: AdminDetails
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,CustId,Home1,Home2,Home3,Cell1,Cell2,Cell3,Work1,Work2,Work3,Email,LastUpdated,CheckBox")] Contact contact, bool checkBox)
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
                NewContact.CheckBox = contact.CheckBox;
                NewContact.LastUpdated = contact.LastUpdated = DateTime.Now;
                _context.Contact.Add(NewContact);
                await _context.SaveChangesAsync();

                if (NewContact.CheckBox == true)
                {
                    return RedirectToAction("Create", "Dependents", new { CustId = NewContact.CustId });
                }
                else
                { 
                        return RedirectToAction("Create", "Deposits", new { CustId = NewContact.CustId });
                }
            }

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", contact.CustId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize]
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
        [Authorize]
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
                    return RedirectToAction("Index", "Customers");
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", contact.CustId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList", "Customers");
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
