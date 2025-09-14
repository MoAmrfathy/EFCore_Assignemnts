using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public int Age { get; set; }
        public int Dep_Id { get; set; }
    }
}
