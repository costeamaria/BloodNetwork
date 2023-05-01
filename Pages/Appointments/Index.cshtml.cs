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
            if (_context.Appointment != null)
            {
                Appointment = await _context.Appointment
                .Include(c => c.Clinic)
                .ThenInclude(c => c.Doctor)
                .Include(c => c.Member).ToListAsync();
               
            }

        }
       
    }
}
