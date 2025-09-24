using Demo.Contexts.CompanyDB;
using Demo.Models.Company;
using Demo.Files.CompanyDbContextSeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using CompanyDbContext context = new CompanyDbContext();

            #region Departments Seed
            var departments = new List<Department>
            {
                new Department { Name = "Finance", HireDate = new DateTime(2025, 1, 10), Serial = 1001 },
                new Department { Name = "Marketing", HireDate = new DateTime(2025, 2, 15), Serial = 1002 },
                new Department { Name = "IT", HireDate = new DateTime(2025, 3, 20), Serial = 1003 }
            };

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(departments);
                context.SaveChanges();
                Console.WriteLine("Departments seeded manually.");
            }
            #endregion

            #region Employees Seed (JSON Seed)
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees ON");

            bool seedResult = CompanyDbContextSeed.dataseed(context);
            Console.WriteLine(seedResult ? "Employees seeded successfully." : "Employees already exist or error.");

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees OFF");
            #endregion

            #region Eager Loading Example
            var firstEmployeeWithDept = context.Employees
                                               .Include(e => e.Department)
                                               .FirstOrDefault();
            if (firstEmployeeWithDept != null)
            {
                Console.WriteLine($"[Eager] Employee: {firstEmployeeWithDept.EmpName}, Department: {firstEmployeeWithDept.Department?.Name}");
            }
            #endregion

            #region Explicit Loading Example
            var anotherEmployee = context.Employees.FirstOrDefault(e => e.Id > 0);
            if (anotherEmployee != null)
            {
                context.Entry(anotherEmployee).Reference(e => e.Department).Load();
                Console.WriteLine($"[Explicit] Employee: {anotherEmployee.EmpName}, Department: {anotherEmployee.Department?.Name}");

                if (anotherEmployee.Department != null)
                {
                    context.Entry(anotherEmployee.Department).Collection(d => d.Employees).Load();
                    Console.WriteLine($"Employees in {anotherEmployee.Department.Name}:");
                    foreach (var emp in anotherEmployee.Department.Employees)
                    {
                        Console.WriteLine($" - {emp.EmpName}");
                    }
                }
            }
            #endregion

            #region Lazy Loading Example
            var lazyEmployee = context.Employees.FirstOrDefault();
            if (lazyEmployee != null)
            {
                Console.WriteLine($"[Lazy] Employee: {lazyEmployee.EmpName}, Department: {lazyEmployee.Department?.Name}");
            }
            #endregion

            #region CRUD Operations Demo

            #region Create
            var newEmployee = new Employee
            {
                EmpName = "New Hire",
                Address = "HQ",
                Password = "Default123!",
                DepartmentId = context.Departments.First().Id
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            Console.WriteLine($"Inserted Employee: {newEmployee.EmpName}");
            #endregion

            #region Update
            newEmployee.EmpName = "Updated Hire";
            context.SaveChanges();
            Console.WriteLine($"Updated Employee Name: {newEmployee.EmpName}");
            #endregion

            #region Delete
            context.Employees.Remove(newEmployee);
            context.SaveChanges();
            Console.WriteLine($"Deleted Employee: {newEmployee.EmpName}");
            #endregion

            #endregion
        }
    }
}
