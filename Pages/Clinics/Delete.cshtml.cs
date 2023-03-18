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
    public class DeleteModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public DeleteModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Clinic Clinic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinic.FirstOrDefaultAsync(m => m.ID == id);

            if (clinic == null)
            {
                return NotFound();
            }
            else 
            {
                Clinic = clinic;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }
            var clinic = await _context.Clinic.FindAsync(id);

            if (clinic != null)
            {
                Clinic = clinic;
                _context.Clinic.Remove(Clinic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
