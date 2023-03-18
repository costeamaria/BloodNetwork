using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;

namespace BloodNetwork.Pages.Adresses
{
    public class DeleteModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public DeleteModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Adress Adress { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Adress == null)
            {
                return NotFound();
            }

            var adress = await _context.Adress.FirstOrDefaultAsync(m => m.ID == id);

            if (adress == null)
            {
                return NotFound();
            }
            else 
            {
                Adress = adress;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Adress == null)
            {
                return NotFound();
            }
            var adress = await _context.Adress.FindAsync(id);

            if (adress != null)
            {
                Adress = adress;
                _context.Adress.Remove(Adress);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
