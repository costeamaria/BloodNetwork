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

namespace BloodNetwork.Pages.Adresses
{
    public class IndexModel : PageModel
    {
        private readonly BloodNetwork.Data.BloodNetworkContext _context;

        public IndexModel(BloodNetwork.Data.BloodNetworkContext context)
        {
            _context = context;
        }

        public IList<Adress> Adress { get; set; } = default!;

        public AdressIndexData AdressData { get; set; }
        public int AdressID { get; set; }
        public int ClinicID { get; set; }

        public async Task OnGetAsync(int? id, int? clinicID)
        {
            AdressData = new AdressIndexData();
            AdressData.Adresses = await _context.Adress
            .Include(i => i.Clinics)
            .OrderBy(i => i.AdressName)
            .ToListAsync();
            if (id != null)
            {
                AdressID = id.Value;
                Adress adress = AdressData.Adresses
                .Where(i => i.ID == id.Value).Single();
                AdressData.Clinics = adress.Clinics;
            }
        }
    }
}
