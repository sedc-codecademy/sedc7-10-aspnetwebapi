using Microsoft.EntityFrameworkCore;

namespace DataModels
{
    public class ToDoAppContext : DbContext
    {
        private readonly string _connectionString;
        public ToDoAppContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany<ToDoItem>()
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
