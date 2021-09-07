using Microsoft.EntityFrameworkCore;
using Sprouts.Models;
using System;

namespace Sprouts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Kid> Kids { get; set; } = default!;
        public DbSet<Study> Studies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Study>()
                .HasOne(p => p.Kid)
                .WithMany(s => s.Studies)
                .HasForeignKey(p => p.KidId);

        
        }

        internal void ExecuteStoreCommand(string v)
        {
            throw new NotImplementedException();
        }
    }
}
