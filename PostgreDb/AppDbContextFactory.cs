using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CrmService.PostgreDb
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Host=127.0.0.1;Port=5432;Database=CrmDB;Username=postgres;Password=183964Asd-")
            .Options;

            return new AppDbContext(opts);
        }
    }
}
