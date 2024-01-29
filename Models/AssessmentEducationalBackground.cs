using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study_Abroad.Models
{
    public class AssessmentEducationalBackground
    {
        [Key]
        public long AssessmentEducationalBackgroundId { get; set; }
        [ForeignKey("AssessmentId")]
        public long AssessmentId { get; set; }
        public virtual Assessment Assessment { get; set; }
        public string Degree { get; set; }
        public DateTime GraduatingYear { get; set; }
        public string CourseDuration { get; set; }
        public string CGPA { get; set; }
        public string Institutions { get; set; }
        public string MajorSubjects { get; set; }
        public bool Status { get; set; }
        [DisplayFormat(DataFormatString = "0:dd-MMM-yy", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();
    }
}
