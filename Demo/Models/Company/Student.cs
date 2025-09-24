using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.Company
{
    public class Student
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Student_Course> Student_Courses { get; set; } 

    }
}
