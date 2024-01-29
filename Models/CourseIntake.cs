using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class CourseIntake
    {
        [Key]
        public long CourseIntakeId { get; set; }
        public string CourseIntakeValue { get; set; }
        [ForeignKey("CourseId")]
        public long CourseId { get; set; }
        public virtual Course Courses { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }

    }
}
