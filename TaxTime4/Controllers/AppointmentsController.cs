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
using TaxTime4.ViewModels;

namespace TaxTime4.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly TaxTime4Context _context;
        private UserManager<IdentityUser> _userManager;

        public AppointmentsController(TaxTime4Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var taxTime4Context = _context.Appointment.Include(a => a.Cust);
            return View(await taxTime4Context.ToListAsync());
        }

        // GET: Cold Call Infomation
        [Authorize]
        public async Task<IActionResult> ColdCall()
        {
            var appointments = _context.Appointment.Include(a => a.Cust)
                .Where(app => app.NextAppt == null)
                .ToList();

            return View(appointments);
        }

        // GET: Appointments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: AdminDetails/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize]
        public IActionResult Create(int custId, Customer customer)
        {
            Appointment pass = new Appointment { CustId = custId };
            
            pass.LastUpdated = DateTime.Now;

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId");
            return View(pass);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustId,LastAppt,NextAppt,LastUpdated")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                Appointment NewAppointment = new Appointment();
                NewAppointment.CustId = appointment.CustId;
                NewAppointment.LastAppt = appointment.LastAppt;
                NewAppointment.NextAppt = appointment.NextAppt;
                NewAppointment.LastUpdated = appointment.LastUpdated = DateTime.Now;
                _context.Appointment.Add(NewAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Customers");
            }

            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", appointment.CustId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            appointment.LastAppt = appointment.NextAppt;
            appointment.NextAppt = null;
            appointment.LastUpdated = DateTime.Now;

            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustId", appointment.CustId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustId,LastAppt,NextAppt,LastUpdated")] Appointment appointment)
        {
            if (id != appointment.CustId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Appointment EditAppointment = new Appointment();
                    EditAppointment.CustId = appointment.CustId;
                    EditAppointment.LastAppt = appointment.LastAppt;
                    EditAppointment.NextAppt = appointment.NextAppt;
                    EditAppointment.LastUpdated = appointment.LastUpdated = DateTime.Now;
                    _context.Appointment.Update(EditAppointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.CustId))
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
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "FirstName", appointment.CustId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Cust)
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminList", "Customers");
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.CustId == id);
        }
    }
}
