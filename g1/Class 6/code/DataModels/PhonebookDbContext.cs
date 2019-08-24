using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions options) : base(options) {}

        public DbSet<DtoUser> Users { get; set; }
        public DbSet<DtoContact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtoContact>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.UserId);
        }
    }
}
