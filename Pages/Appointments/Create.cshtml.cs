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
           
            var memberList = _context.Member
                .Select(y => new
                {
                    y.ID,
                    MemberFullName = " Email: " + y.Email + ", Nume: "+ y.FullName+ " "
                });
              

            ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberFullName");
            ViewData["ClinicID"] = new SelectList(clinicList, "ID", "ClinicFullName");
            

            return Page();
        }
 
        [BindProperty]
        public Appointment Appointment { get; set; }
        [BindProperty]
        public DateTime StartTime { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }
            
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

       

    }
}
