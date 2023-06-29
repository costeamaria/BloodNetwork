using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BloodNetwork.Data;
using BloodNetwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace BloodNetwork.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public CreateModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            var clinicList = _context.Clinic
                .Select(x => new
                {
                    x.ID,
                    ClinicFullName = x.Name + ", doctor: " + x.Doctor.DoctorName + " "
                });

            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;

                var memberList = _context.Member
                    .Where(y => y.Email == email) //lista membrilor pe baza adresei de email a utilizatorului autentificat in aplicatie
                    .Select(y => new
                    {
                        y.ID,
                        MemberFullName = " Email: " + y.Email + ", Nume: " + y.FullName + " "
                    });

                ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberFullName");
            }
            ViewData["ClinicID"] = new SelectList(clinicList, "ID", "ClinicFullName");
            

            return Page();
        }
 
        [BindProperty]
        public Appointment Appointment { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Appointment == null || Appointment == null)
            {
                return Page();
            }
            var existingAppointment = await _context.Appointment.FirstOrDefaultAsync(a =>
            a.MemberID == Appointment.MemberID &&
            a.StartTime == Appointment.StartTime &&
            a.ClinicID == Appointment.ClinicID);

            if (existingAppointment != null)
            {
                // Programarea duplicată a fost găsită, tratați această situație aici
                ModelState.AddModelError(string.Empty, "O programare cu același medic, dată și oră există deja.");
                // lista de opțiuni pentru medic și data,ora disponibila
                var email = User.Identity.Name;

                var member = await _context.Member.FirstOrDefaultAsync(y => y.Email == email);
                if (member != null)
                {
                    ViewData["MemberID"] = new SelectList(new[] { member }, "ID", "Email");
                }
                ViewData["ClinicID"] = new SelectList(_context.Clinic, "ID", "Name");
                return Page();
            }

            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
