using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class GmatTest
    {
        [Key]
        public int GmatId { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentProfile StudentProfile { get; set; }
        public bool GMAT_Exame { get; set; }
        public DateTime? GMAT_Exam_Date { get; set; }
        public double GMAT_Verbal { get; set; }
        public double GMAT_Verbal_Rank { get; set; }
        public double GMAT_Quantitative { get; set; }
        public double GMAT_Quantitative_Rank { get; set; }
        public double GMAT_Writting { get; set; }
        public double GMAT_Writting_Rank { get; set; }
        public double GMAT_Total { get; set; }
        public double GMAT_Total_Rank { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
