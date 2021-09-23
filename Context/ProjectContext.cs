
using SafeCon.Models;
using System;
using System.Data.Entity;

namespace SafeCon.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server= (localdb)\\mssqllocaldb; Database=SafeCon; Integrated Security=True;";
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProjectContext>(null);
            base.OnModelCreating(modelBuilder);

        }
    }
}
