using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class ApplicationDetails
    {
        [Key]
        public int AppDetailId { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }       
        public virtual StudentProfile StudentProfile { get; set; }
        [ForeignKey("UniversityId")]
        public int UniversityId { get; set; }       
        public virtual University University { get; set; }
        [ForeignKey("CourseId")]
        public long? CourseId { get; set; }      
        public virtual Course Course { get; set; }
        [ForeignKey("CourseIntakeId")]
        public long? CourseIntakeId { get; set; }   
        public virtual CourseIntake CourseIntake { get; set; }
        [ForeignKey("ReqId")]
        public int ReqId { get; set; }
        public virtual AppRequairement Requairement { get; set; }

    }
}
