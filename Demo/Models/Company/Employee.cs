using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Demo.Models.Company
{

    public class Employee
    {
        public int Id { get; set; }

        [JsonPropertyName("EmpName")]
        public string EmpName { get; set; }

        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public int? DepartmentId { get; set; }

        public Grade Grade { get; set; }
        public virtual Department Department { get; set; }
    }

    public enum Grade
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        F = 5
    }

}

