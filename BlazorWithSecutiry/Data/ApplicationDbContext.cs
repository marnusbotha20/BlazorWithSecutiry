using System;
using System.Collections.Generic;
using System.Text;
using BlazorWithSecutiry.DAL;
using BlazorWithSecutiry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithSecutiry.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cities> Cities { get; internal set; }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<EmployeeCourse> EmployeeCourse { get; set; }
        public DbSet<ContactUsDetails> ContactUsDetails { get; set; }

        public IEnumerable<string> Migrate()
        {
            yield return $"Starting database migration for {nameof(ApplicationDbContext)}";
            foreach (var item in RunFolderScripts("before-migrate"))
            {
                yield return item;
            }

            foreach (var item in ContextMigration.Migrate(this))
            {
                yield return item;
            }

            foreach (var item in RunFolderScripts("after-migrate"))
            {
                yield return item;
            }


            //yield return "Seeding data...";

            //foreach (var item in this.EnsureSeeded())
            //{
            //    yield return item;
            //}

            //foreach (var item in RunFolderScripts("after-seed"))
            //{
            //    yield return item;
            //}

            yield return "Database migration complete";
        }

        private IEnumerable<string> RunFolderScripts(string subFolder)
        {
            var res = this.ExecuteMigrationScripts($"Scr\\Aw\\{subFolder}\\run-once", true);
            foreach (var item in res) yield return item;

            var runAlways = this.ExecuteMigrationScripts($"Scr\\Aw\\{subFolder}\\run-always", false);
            foreach (var item in runAlways) yield return item;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeCourse>().HasKey(bc => new { bc.CourseId, bc.EmployeeId });

            modelBuilder.Entity<Company>().HasKey(c => new { c.CompanyId });

            modelBuilder.Entity<EmployeeCourse>()
                .HasOne(bc => bc.Courses)
                .WithMany(b => b.EmployeeCourse)
                .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<EmployeeCourse>()
                .HasOne(bc => bc.Employee)
                .WithMany(c => c.EmployeeCourse)
                .HasForeignKey(bc => bc.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(bc => bc.Company)
                .WithMany(s => s.Employee);

            var Company = new Company()
            {
                CompanyId = 1,
                Name = "atWork Internet Solutions"
            };

            modelBuilder.Entity<Company>().HasData(Company);

            modelBuilder.Entity<Cities>().HasData(new Cities
            {
                CityId = 1,
                CityName = "Pretoria"
            },
            new Cities
            {
                CityId = 2,
                CityName = "Johannesburg"
            });

            var EmployeeList = new List<EmployeeCourse>()
            {
                new EmployeeCourse()
                {
                    EmployeeId = 1,
                    CourseId =1
                },
                new EmployeeCourse()
                {
                    EmployeeId = 1,
                    CourseId =2
                },
                new EmployeeCourse()
                {
                    EmployeeId = 2,
                    CourseId =1
                }
            };

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Uncle",
                    LastName = "Bob",
                    Email = "uncle.bob@gmail.com",
                    DateOfBirth = new DateTime(1979, 04, 25),
                    PhoneNumber = "999-888-7777",
                    City = "Pretoria",
                    Gender = Gender.Male.ToString(),
                    Department = "IT",
                    //Company = Company

                }, new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Jan",
                    LastName = "Kirsten",
                    Email = "jan.kirsten@gmail.com",
                    DateOfBirth = new DateTime(1981, 07, 13),
                    PhoneNumber = "111-222-3333",
                    City = "Johannesburg",
                    Gender = Gender.Male.ToString(),
                    Department = "Management",
                    //Company = Company
                }
            );

            modelBuilder.Entity<Courses>().HasData(new Courses
            {
                CourseId = 1,
                CourseName = "Coding 101",
                Credits = 10
            },
            new Courses
            {
                CourseId = 2,
                CourseName = "Coding 201",
                Credits = 20
            });

            modelBuilder.Entity<EmployeeCourse>().HasData(EmployeeList);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MARNUS-LT;Database=BlazorSecurity;Trusted_Connection=True;");
        }
    }
}
