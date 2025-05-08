using CrmService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CrmService.PostgreDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<ServiceItem> Services { get; set; }
    }
}
