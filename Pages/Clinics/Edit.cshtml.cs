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

namespace BloodNetwork.Pages.Clinics
{
    public class EditModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public EditModel(BloodNetwork.Data.BloodNetworkContext context)
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

            var clinic =  await _context.Clinic.FirstOrDefaultAsync(m => m.ID == id);
            if (clinic == null)
            {
                return NotFound();
            }
            Clinic = clinic;
            ViewData["AdressID"] = new SelectList(_context.Set<Adress>(), "ID", "AdressName");
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

            _context.Attach(Clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(Clinic.ID))
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

        private bool ClinicExists(int id)
        {
          return (_context.Clinic?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
