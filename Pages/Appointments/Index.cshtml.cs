using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace BloodNetwork.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public IndexModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get;set; } = default!;
        

        public async Task OnGetAsync()
        {
            var isAdmin = User.Identity.Name == "admin@gmail.com";

            if (_context.Appointment != null)
            {
                if (isAdmin)
                {
                    // Retrieve all appointments if user is an admin
                        Appointment = await _context.Appointment
                        .Include(appt => appt.Clinic)
                        .ThenInclude(appt => appt.Doctor)
                        .Include(appt => appt.Member)
                        .ToListAsync();
                }
                else

                {
                    Appointment = await _context.Appointment
                    .Include(appt => appt.Clinic)
                    .ThenInclude(appt => appt.Doctor)
                    .Include(appt => appt.Member)
                    .Where(appt => appt.Member.Email == User.Identity.Name).ToListAsync();
                }

            }

        }

    }
}
