using Ferdo.Data.Entities;
using System;
using System.Data.Entity;

namespace Ferdo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public IDbSet<User> Users { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Address> Address { get; set; }

        public ApplicationDbContext()
            : base("FerdoConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasMany(x => x.Users).WithMany();
            base.OnModelCreating(modelBuilder);
        }
    }
}
