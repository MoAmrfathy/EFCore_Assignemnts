using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    [Table("Course_Inst")]
    public class Course_Inst
    {
        [Key]
        public int Inst_ID { get; set; }

        public int Course_ID { get; set; }

        [MaxLength(50)]
        public string Evaluate { get; set; }
    }
}
