using Microsoft.EntityFrameworkCore;

namespace DataModels
{
    public class ToDoAppDbContext : DbContext
    {
        private readonly string _connectionString;

        public ToDoAppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ToDoAppDbContext()
        {
            
        }

        public DbSet<DtoUser> Users { get; set; }
        public DbSet<DtoToDoItem> ToDoList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DtoToDoItem>()
                .HasOne(u => u.User)
                .WithMany(b => b.ToDoList)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}
