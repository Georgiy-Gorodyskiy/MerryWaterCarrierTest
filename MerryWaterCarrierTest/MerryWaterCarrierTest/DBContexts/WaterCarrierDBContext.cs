using MerryWaterCarrierTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MerryWaterCarrierTest.DBContexts
{
    public class WaterCarrierDBContext : DbContext
    {
        public WaterCarrierDBContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(x => x.Department)
                  .WithMany(x => x.Employees)
                  .HasForeignKey(x => x.DepartmentId);
                entity.Property(x => x.DepartmentId)
                  .HasDefaultValue(null);
            });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "server=localhost;user=root;password=12345678;database=MerryWaterCarrierDatabase.db;";
            ServerVersion vesrion = ServerVersion.AutoDetect(connection);
            optionsBuilder.UseMySql(connection, vesrion);
        }
    }
}
