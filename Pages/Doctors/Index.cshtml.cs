using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Data;
using BloodNetwork.Models;
using BloodNetwork.Models.ViewModels;

namespace BloodNetwork.Pages.Doctors
{
    public class IndexModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public IndexModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get;set; } = default!;
        public DoctorIndexData DoctorData { get; set; }
        public int DoctorID { get; set; }
        public int ClinicID { get; set; }

        public async Task OnGetAsync(int? id, int? doctorID)
        {
            DoctorData = new DoctorIndexData();
            DoctorData.Doctors = await _context.Doctor
            .Include(i => i.Clinics)
            .OrderBy(i => i.DoctorName)
            .ToListAsync();
            if (id != null)
            {
                DoctorID = id.Value;
                Doctor doctor = DoctorData.Doctors
                .Where(i => i.ID == id.Value).Single();
                DoctorData.Clinics = doctor.Clinics;
            }
        }
    }
}
