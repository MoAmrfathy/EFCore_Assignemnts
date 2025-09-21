using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.ITI
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int? Ins_ID { get; set; }
        public DateTime HiringDate { get; set; }


        public Instructor Manager { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }


    }
}
