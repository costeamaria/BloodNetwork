using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BloodNetwork.Data;
using BloodNetwork.Models;

namespace BloodNetwork.Pages.Clinics
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
            ViewData["AdressID"] = new SelectList(_context.Set<Adress>(), "ID", "AdressName");
            return Page();
        }

        [BindProperty]
        public Clinic Clinic { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Clinic == null || Clinic == null)
            {
                return Page();
            }

            _context.Clinic.Add(Clinic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
