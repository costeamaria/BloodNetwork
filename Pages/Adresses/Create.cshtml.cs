using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BloodNetwork.Data;
using BloodNetwork.Models;

namespace BloodNetwork.Pages.Adresses
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
            return Page();
        }

        [BindProperty]
        public Adress Adress { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Adress == null || Adress == null)
            {
                return Page();
            }

            _context.Adress.Add(Adress);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
