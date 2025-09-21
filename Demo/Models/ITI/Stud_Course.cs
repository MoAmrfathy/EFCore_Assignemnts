using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.ITI
{
    [Table("Stud_Course")]
    public class Stud_Course
    {
        [Key]
        public int stud_ID { get; set; }
        public Student Student { get; set; }

        public int Course_ID { get; set; }
        public Course Course { get; set; }

        public decimal Grade { get; set; }
    }
}
