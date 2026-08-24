using Consumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Data;

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
       .HasForeignKey(e => e.UnitId);

        modelBuilder.Entity<Assets>()
           .HasOne(e => e.assetLiveStatus)
           .WithOne(e => e.Assets)
           .HasForeignKey<AssetLiveStatus>(e => e.AssetId);
    }
}


     