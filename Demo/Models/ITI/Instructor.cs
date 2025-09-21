using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.ITI
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Bouns { get; set; }
        public decimal Salary { get; set; }

        [MaxLength(200)]
        public string Adress { get; set; }

        public decimal HourRate { get; set; }
        public int Dept_ID { get; set; }


        public Department Department { get; set; }

        public ICollection<Course_Inst> Course_Insts { get; set; }
    }
}
