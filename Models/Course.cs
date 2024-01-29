using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Course
    {
        [Key]
        public long CourseId { get; set; }

        [ForeignKey("DisiplineId")]
        public int DisiplineId { get; set; }
        public virtual Disipline Disiplines { get; set; }
        [ForeignKey("CampusId")]
        public int CampusId { get; set; }
        public virtual Campus Campuses { get; set; }
        //is ki zarort ni
        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public virtual State States { get; set; }

        [Required]
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
        [ForeignKey("Program_Id")]
        public int Program_Id { get; set; }
        public virtual ProgramLevel ProgramLevels { get; set; }
        //ye ni ana
        [Display(Name = "Intake")]
        public string CourseIntake { get; set; }
        [Display(Name = "Mode")]
        public string CourseMode { get; set; }
        [Display(Name = "Tution Fee")]
        public string CourseTutionFee { get; set; }
        [Display(Name = "Duration")]
        public string CourseDuration { get; set; }
        //public string State_Name { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
        // is me scholarship   and application deadline ani hain
        [Display(Name = "Scholarshipe")]
        public string CourseScholarship { get; set; }
        [Display(Name = "Application Deadline")]
        [DataType(DataType.Date)]
        public DateTime CourseApplicationDeadline { get; set; }
        [Display(Name = "Overview")]
        public string CourseOverview { get; set; }
        [Display(Name = "Learning Outcomes")]
        public string CourseLearningOutcome { get; set; }
        [Display(Name = "Start Date And Price")]
        public string CourseStartDateAndPrice { get; set; }
        [Display(Name = "How To Apply")]
        public string CourseHowToApply { get; set; }
        [Display(Name = "Reviews And Rankings")]
        public string CourseReviewAndRanking { get; set; }
    }
}
