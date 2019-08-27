using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    public class PhonebookDbContext : DbContext
    {
        public PhonebookDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<DtoContact> Contacts { get; set; }
        public DbSet<DtoUser> Users { get; set; }
    }
}
