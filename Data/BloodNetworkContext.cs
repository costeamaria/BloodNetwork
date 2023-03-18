using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BloodNetwork.Models;

namespace BloodNetwork.Data
{
    public class BloodNetworkContext : DbContext
    {
        public BloodNetworkContext (DbContextOptions<BloodNetworkContext> options)
            : base(options)
        {
        }

        public DbSet<BloodNetwork.Models.Clinic> Clinic { get; set; } = default!;

        public DbSet<BloodNetwork.Models.Adress>? Adress { get; set; }

        public DbSet<BloodNetwork.Models.City>? City { get; set; }

        public DbSet<BloodNetwork.Models.Doctor>? Doctor { get; set; }
    }
}
