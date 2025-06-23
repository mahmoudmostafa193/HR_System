using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRSystem.Models;
namespace HRSystem.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Attendance> Attendances { get; set; } = null;

        public DbSet<Salaries> Salaries { get; set; } = null;

        public DbSet<Approvals> Approvals { get; set; } = null;
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; } = null;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
     .HasOne(e => e.Address)
     .WithMany() // أو .WithOne() حسب العلاقة
     .HasForeignKey(e => e.AddressId)
     .OnDelete(DeleteBehavior.Restrict); // لتجنب مشاكل الـ FK
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Attendances)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
              .HasMany(e => e.Salaries)
              .WithOne(s => s.Employee)
              .HasForeignKey(s => s.EmployeeId)
              .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
