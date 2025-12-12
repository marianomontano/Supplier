using Microsoft.EntityFrameworkCore;

namespace Supplier.Core.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SettingsModel>().HasKey(x => x.Id);
    }

    public DbSet<SettingsModel> Settings { get; set; }
}
