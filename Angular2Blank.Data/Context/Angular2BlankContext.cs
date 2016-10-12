using System.ComponentModel;
using Angular2Blank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Angular2Blank.Data.Context
{
    public class Angular2BlankContext: DbContext
    {
        public Angular2BlankContext(DbContextOptions options): base(options)
        {
            this.Init();
        }

        //Todo fix for RC1
        public void Init()
        {
            this.Database.EnsureCreated();
            //this.Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}


