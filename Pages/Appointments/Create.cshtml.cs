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
                    MemberFullName = " Email: " + y.Email + " "
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
        private List<Tuple<DateTime, DateTime>> GetAvailableTimeSlots(int DurationInMinutes, DateTime start, DateTime end, List<Appointment> existingAppointments)
        {
            var availableTimeSlots = new List<Tuple<DateTime, DateTime>>();

            // Calculate the total number of minutes in the appointment window
            var totalMinutes = (int)(end - start).TotalMinutes;

            // Calculate the number of time slots that can fit within the appointment window
            var numTimeSlots = totalMinutes / DurationInMinutes;

            // Initialize a list of time slots starting at the appointment window start time
            var timeSlots = new List<DateTime>();
            var currentTime = start;
            for (int i = 0; i < numTimeSlots; i++)
            {
                timeSlots.Add(currentTime);
                currentTime = currentTime.AddMinutes(DurationInMinutes);
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
                    if (timeSlot >= startTime && timeSlot.AddMinutes(DurationInMinutes) <= endTime)
                    {
                        timeSlots.RemoveAt(i);
                        i--;
                    }
                }
            }

            // Filter out time slots that are less than the appointment duration
            timeSlots = timeSlots.Where(slot => (slot.AddMinutes(DurationInMinutes) - start).TotalMinutes >= DurationInMinutes).ToList();

            // Convert each time slot to a tuple of start and end times
            foreach (var timeSlot in timeSlots)
            {
                var endTime = timeSlot.AddMinutes(DurationInMinutes);
                var tuple = Tuple.Create(timeSlot, endTime);
                availableTimeSlots.Add(tuple);
            }

            return availableTimeSlots;
        }

    }
}
