using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;
using Microsoft.CodeAnalysis;

namespace BloodNetwork.Pages.Clinics
{
    public class IndexModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public IndexModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IList<Clinic> Clinic { get; set; } = default!;
        public ClinicData ClinicD { get; set; }
        public int ClinicID { get; set; }
        public int CategoryID { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string searchString)
        {
            ClinicD = new ClinicData();
            CurrentFilter = searchString;

            ClinicD.Clinics = await _context.Clinic
            .Include(c => c.Adress)
            .Include(c => c.Doctor)
            .Include(c => c.ClinicCategories)
            .ThenInclude(c => c.Category)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                ClinicD.Clinics = ClinicD.Clinics.Where(s => s.Doctor.DoctorName.Contains(searchString));

                if (id != null)
                {
                    ClinicID = id.Value;
                    Clinic clinic = ClinicD.Clinics
                    .Where(i => i.ID == id.Value).Single();
                    ClinicD.Categories = clinic.ClinicCategories.Select(s => s.Category);
                }
            }
        }
    }
}
