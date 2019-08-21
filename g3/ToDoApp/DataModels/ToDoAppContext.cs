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
            builder.Entity<DtoUser>()
                .HasMany<DtoToDoItem>()
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<DtoUser> Users { get; set; }
        public DbSet<DtoToDoItem> ToDoItems { get; set; }
    }
}
