using Microsoft.EntityFrameworkCore;
using RemontKotlov.Entities;

namespace RemontKotlov.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MetaData> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
