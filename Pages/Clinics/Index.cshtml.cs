using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;

namespace BloodNetwork.Pages.Clinics
{
    public class IndexModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public IndexModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IList<Clinic> Clinic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Clinic != null)
            {
                Clinic = await _context.Clinic
                    .Include(c => c.Adress)
                    .ToListAsync();
            }
        }
    }
}
