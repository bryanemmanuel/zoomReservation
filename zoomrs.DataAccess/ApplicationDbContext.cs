using zoomrs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace zoomrs.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>();
            //builder.Properties<TimeOnly>()
            //    .HaveConversion<TimeOnlyConverter>();
        }
        //public ApplicationDbContext()
        //{

        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // account =  from models->account.cs, accountstbl =  name of table
        //public DbSet<coffeeList> coffeetbl { get; set; }
        //public DbSet<sizeList> sizetbl { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<DisableDate> DisableDate { get; set; }
        //public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Department)
                .WithMany()
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }


}
