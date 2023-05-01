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
                    ClinicFullName = x.Name + ", doctor: " + x.Doctor.DoctorName + ", categoria: "+ x.ClinicCategories + " " 
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

        [HttpGet]
        public IActionResult GetAvailableTimeSlots(int duration, DateTime start, DateTime end)
        {
            var availableTimeSlots = GetAvailableTimeSlots(duration, start, end);
            return Ok(availableTimeSlots);
        }

        private IActionResult Ok(IActionResult availableTimeSlots)
        {
            throw new NotImplementedException();
        }
 

        [BindProperty]
        public Appointment Appointment { get; set; }

        private List<DateTime> GetAvailableTimeSlots(int duration, DateTime start, DateTime end, List<Appointment> existingAppointments)
        {
            var availableTimeSlots = new List<DateTime>();

            // Calculate the total number of minutes in the appointment window
            var totalMinutes = (int)(end - start).TotalMinutes;

            // Calculate the number of time slots that can fit within the appointment window
            var numTimeSlots = totalMinutes / duration;

            // Initialize a list of time slots starting at the appointment window start time
            var timeSlots = new List<DateTime>();
            var currentTime = start;
            for (int i = 0; i < numTimeSlots; i++)
            {
                timeSlots.Add(currentTime);
                currentTime = currentTime.AddMinutes(duration);
            }

            // Iterate through the existing appointments and remove their time slots from the list of available time slots
            foreach (var appointment in existingAppointments)
            {
                var startTime = appointment.StartTime;
                var endTime = appointment.EndTime;

                // Loop through each time slot and remove it if it falls within the existing appointment
                for (int i = 0; i < timeSlots.Count; i++)
                {
                    var timeSlot = timeSlots[i];
                    if (timeSlot >= startTime && timeSlot.AddMinutes(duration) <= endTime)
                    {
                        timeSlots.RemoveAt(i);
                        i--;
                    }
                }
            }

            // Add the remaining time slots to the list of available time slots
            availableTimeSlots.AddRange(timeSlots);

            return availableTimeSlots;
        }

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
