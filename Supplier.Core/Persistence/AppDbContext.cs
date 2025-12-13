using Microsoft.EntityFrameworkCore;

namespace Supplier.Core.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SettingsModel>(entity =>
        {
            entity.ToTable("Settings");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Markup)
                  .IsRequired();

            entity.Property(x => x.ApiUrl)
                  .IsRequired();
        });
    }

    public DbSet<SettingsModel> Settings { get; set; }
}
