using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp1
{

    class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        //public string Test { get; set; }
    }

    class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EFCoreTstDb;Trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().Property(e => e.Description).HasMaxLength(250);
            modelBuilder.Entity<Department>().Property(e => e.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Department>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Department>().Property(e => e.Name).HasMaxLength(250);

            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Gender).HasMaxLength(10).HasDefaultValue("Male")
                .IsRequired();
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                //context.Employee.Add(new Employee
                //{
                //    Name = "Emp2"
                //});
                //context.SaveChanges();

                var employee = context.Employee.First(w => w.Name == "Emp2");
                employee.Gender = "Male";
                //context.Employee.Update(employee);
                context.SaveChanges();

                //var std = context.Employee.First();
                //context.Employee.Remove(std);

                var gender = context.Employee.Where(w => w.Name == "Emp2").Select(s => s.Gender).FirstOrDefault();
                Console.WriteLine(gender);
                Console.ReadLine();

            }
        }
    }
}
