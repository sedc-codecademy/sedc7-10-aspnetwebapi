using BooksModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace BooksDataAccess
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options)
           : base(options)
        {
        }

        public BooksContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity
                    .HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorID);
            });
        }

    }
}
