using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class LanguageTest
    {
        [Key]
        public int LanguageId { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentProfile StudentProfile { get; set; }
        public string Language_Type { get; set; }
        public DateTime? Exam_Date { get; set; }
        public double Reading_Score { get; set; }
        public double Listening_Score { get; set; }
        public double Writing_Score { get; set; }
        public double Speaking_Score { get; set; }
        public double Overall_Score { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
