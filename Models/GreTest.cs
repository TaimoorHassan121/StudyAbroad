using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class GreTest
    {
        [Key]
        public int GreId { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentProfile StudentProfile { get; set; }
        public bool GRE_Exame { get; set; }
        public DateTime? GRE_Exam_Date { get; set; }
        public double GRE_Verbal { get; set; }
        public double GRE_Verbal_Rank { get; set; }
        public double GRE_Quantitative { get; set; }
        public double GRE_Quantitative_Rank { get; set; }
        public double GRE_Writting { get; set; }
        public double GRE_Writting_Rank { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
