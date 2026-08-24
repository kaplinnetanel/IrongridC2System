using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Api.Data;

public class ApplicationDbContext : DbContext
{


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    public DbSet<Units> Units { get; set; }
    public DbSet<Assets> Assets { get; set; }
    public DbSet<AssetLiveStatus> AssetLiveStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Units>()
       .HasMany(e => e.Assets)
       .WithOne(e => e.Units)
       .HasForeignKey(e => e.UnitId)
       .IsRequired();

        modelBuilder.Entity<Assets>()
           .HasOne(e => e.assetLiveStatus)
           .WithOne(e => e.Assets)
           .HasForeignKey<AssetLiveStatus>(e => e.AssetId);
    }
}
