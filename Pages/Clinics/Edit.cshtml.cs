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
using BloodNetwork.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Adress = BloodNetwork.Models.Adress;
using Doctor = BloodNetwork.Models.Doctor;

namespace BloodNetwork.Pages.Clinics
{
    [Authorize(Roles = "Admin")]
    public class EditModel : ClinicCategoriesPageModel
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

            Clinic = await _context.Clinic
                 .Include(c => c.Adress)
                 .Include(c => c.ClinicCategories).ThenInclude(p => p.Category)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ID == id);

            var clinic = await _context.Clinic.FirstOrDefaultAsync(m => m.ID == id);
            if (clinic == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Clinic);
            Clinic = clinic;
            ViewData["AdressID"] = new SelectList(_context.Set<Adress>(), "ID", "AdressName");
            ViewData["DoctorID"] = new SelectList(_context.Set<Doctor>(), "ID", "DoctorName");
          
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinicToUpdate = await _context.Clinic
            .Include(i => i.Adress)
            .Include(i => i.ClinicCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (clinicToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Clinic>(clinicToUpdate, "Clinic",
             i => i.Name, i => i.Quantity,
             i => i.Phone, i => i.AdressID, i => i.DoctorID))
            {
                UpdateClinicCategories(_context, selectedCategories, clinicToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Clinics care
            //este editata
            UpdateClinicCategories(_context, selectedCategories, clinicToUpdate);
            PopulateAssignedCategoryData(_context, clinicToUpdate);
            return Page();
        }
    }
}
