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
                    MemberFullName = " Email: " + y.Email + " "
                });

            ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberFullName");
            ViewData["ClinicID"] = new SelectList(clinicList, "ID", "ClinicFullName");

            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; }
        public string sttime { get; set; }
        public void DisplayTime()
        {
            string start_time = "10:00AM";
            string end_time = "17:00PM";
            int minutes = 15;
            List<TimeList> dateTimes = new List<TimeList>();

            DateTime startdatetime = Convert.ToDateTime(start_time);
            DateTime enddatetime = Convert.ToDateTime(end_time);
            TimeSpan timeinterval = enddatetime.Subtract(startdatetime);

            int totalminutes = Convert.ToInt32(timeinterval.TotalMinutes);

            int no_of_time_slote = totalminutes / minutes;
            for(int i = 0; i < no_of_time_slote; i++)
            {
               TimeList obj = new TimeList();
                startdatetime = startdatetime.AddMinutes(minutes);
                obj.sttime = startdatetime.ToString("hh:mm tt");
                dateTimes.Add(obj);
            }
           
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DisplayTime();
                return Page();
            }
            
            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
