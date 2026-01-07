using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
//using System.Data.OleDb;
//using System.Data.Odbc;


namespace Orm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated(); // Създава базата и таблиците, ако не съществуват

                var employees = new List<Employee> // Дефинираме employees преди да я използваме
                {
                    new Employee { Name = "Ivan", Age = 32, Position = "Mechanic" },
                    new Employee { Name = "Ivan Petrov", Age = 30, Position = "Software Engineer" },
                    new Employee { Name = "Maria Georgieva", Age = 28, Position = "HR Manager" },
                    new Employee { Name = "Georgi Dimitrov", Age = 35, Position = "Team Lead" },
                    new Employee { Name = "Stoyan Ivanov", Age = 40, Position = "CEO" },
                    new Employee { Name = "Dimityr Kozarev", Age = 17, Position = "Student" },
                    new Employee { Name = "Dimityr Sarikov", Age = 17, Position = "Student" },
                    new Employee { Name = "Teodor Sbirkov", Age = 17, Position = "Student" }
                };

                if (!context.Employees.Any()) // Проверява дали таблицата е празна
                {
                    context.Employees.AddRange(employees);
                    context.SaveChanges();
                    Console.WriteLine("Employees added successfully!");
                }
                else
                {
                    Console.WriteLine("Employees already exist in the database.");
                }

                // Извеждаме всички записи от базата за проверка
                foreach (var emp in context.Employees)
                {
                    Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Position: {emp.Position}");
                }
            }
        }
    }

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Position { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EmployeesDB;Trusted_Connection=True;TrustServerCertificate=True;");
            // Променен connection string за LocalDB
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmployeesDB;Trusted_Connection=True;");
        }
    }
}
