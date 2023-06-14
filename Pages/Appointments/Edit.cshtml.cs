using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BloodNetwork.Pages.Appointments
{
    public class EditModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public EditModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Member)
                .Include(a => a.Clinic)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (appointment == null)
            {
                return NotFound();
            }

            Appointment = appointment;

            var loggedInUserEmail = User.Identity.Name;
            var loggedInMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == loggedInUserEmail);

            if (loggedInMember != null && loggedInMember.Email == "admin@gmail.com")
            {
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "Email");
            }
            else if (loggedInMember != null)
            {
                ViewData["MemberID"] = new SelectList(new[] { loggedInMember }, "ID", "Email");
            }

            ViewData["ClinicID"] = new SelectList(_context.Clinic, "ID", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingAppointment = await _context.Appointment.FirstOrDefaultAsync(a =>
                a.ID != Appointment.ID &&
                a.ClinicID == Appointment.ClinicID &&
                a.StartTime == Appointment.StartTime);

            if (existingAppointment != null)
            {
                // Ora selectată se suprapune cu o altă programare existentă în aceeași clinică
                ModelState.AddModelError(string.Empty, "Ora selectată se suprapune cu o altă programare existentă în aceeași clinică.");
                var loggedInUserEmail = User.Identity.Name;
                var loggedInMember = await _context.Member.FirstOrDefaultAsync(m => m.Email == loggedInUserEmail);

                if (loggedInMember != null && loggedInMember.Email == "admin@gmail.com")
                {
                    ViewData["MemberID"] = new SelectList(_context.Member, "ID", "Email");
                }
                else if (loggedInMember != null)
                {
                    ViewData["MemberID"] = new SelectList(new[] { loggedInMember }, "ID", "Email");
                }

                ViewData["ClinicID"] = new SelectList(_context.Clinic, "ID", "Name");
                return Page();
            }

            _context.Attach(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointment?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
