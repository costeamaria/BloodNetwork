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
    public class CreateModel : ClinicCategoriesPageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public CreateModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DoctorID"] = new SelectList(_context.Set<Doctor>(), "ID", "DoctorName");
            ViewData["AdressID"] = new SelectList(_context.Set<Adress>(), "ID", "AdressName");
            var clinic = new Clinic();
            clinic.ClinicCategories = new List<ClinicCategory>();
            PopulateAssignedCategoryData(_context, clinic);
            return Page();
        }

        [BindProperty]
        public Clinic Clinic { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newClinic = new Clinic();
            if (selectedCategories != null)
            {
                newClinic.ClinicCategories = new List<ClinicCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new ClinicCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newClinic.ClinicCategories.Add(catToAdd);
                }
            }

            Clinic.ClinicCategories = newClinic.ClinicCategories;
            _context.Clinic.Add(Clinic);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            PopulateAssignedCategoryData(_context, newClinic);
            return Page();
        }
    }
}
