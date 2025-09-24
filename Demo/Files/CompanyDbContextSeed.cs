using Demo.Contexts.CompanyDB;
using Demo.Models.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Demo.Files.CompanyDbContextSeed
{
    internal static class CompanyDbContextSeed
    {
        public static bool dataseed(CompanyDbContext dbContext)
        {
            try
            {
                SeedDepartments(dbContext, "departments.json");


                Console.WriteLine($"Employee count in DB: {dbContext.Employees.Count()}");
                if (dbContext.Employees.Any())
                {
                    return false;
                }


                SeedEmployees(dbContext, "employees.json");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                return false;
            }
        }

        private static void SeedDepartments(CompanyDbContext dbContext, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Department JSON not found: {filePath}");
                return;
            }

            var departmentData = File.ReadAllText(filePath);
            var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);

            if (departments?.Any() != true)
                return;

            foreach (var dept in departments)
            {
                if (!dbContext.Departments.Any(d => d.Name == dept.Name))
                    dbContext.Departments.Add(dept);
            }

            dbContext.SaveChanges();
        }

        private static void SeedEmployees(CompanyDbContext dbContext, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Employee JSON not found: {filePath}");
                return;
            }

            var employeeData = File.ReadAllText(filePath);
            var employeesFromJson = JsonSerializer.Deserialize<List<JsonEmployee>>(employeeData);

            if (employeesFromJson?.Any() != true)
                return;

            var departmentIds = dbContext.Departments
                                         .AsNoTracking()
                                         .Select(d => d.Id)
                                         .ToList();

            var existingEmployees = dbContext.Employees
                                             .AsNoTracking()
                                             .Select(e => new { e.EmpName, e.DepartmentId })
                                             .ToList();

            foreach (var je in employeesFromJson)
            {
                if (existingEmployees.Any(e => e.EmpName == je.EmpName && e.DepartmentId == je.DepartmentId))
                    continue;

                var emp = new Employee
                {
                    EmpName = je.EmpName,
                    Age = je.Age,
                    Email = je.Email,
                    Salary = je.Salary,
                    Address = $"{je.EmpAddress?.Street}, {je.EmpAddress?.City}, {je.EmpAddress?.Country}" ?? "Unknown",
                    Password = string.IsNullOrEmpty(je.Password) ? "Default123!" : je.Password,
                    DepartmentId = MapDepartment(je.DepartmentId, departmentIds)
                };

                dbContext.Employees.Add(emp);
            }
            dbContext.SaveChanges();
        }


        private static int MapDepartment(int deptIdFromJson, List<int> validDeptIds)
        {
            int index = Math.Abs(deptIdFromJson) - 1;
            return (index >= 0 && index < validDeptIds.Count) ? validDeptIds[index] : validDeptIds.First();
        }
    }

    internal class JsonEmployee
    {
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public JsonAddress EmpAddress { get; set; }
        public int DepartmentId { get; set; }
        public string Password { get; set; }
    }

    internal class JsonAddress
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
