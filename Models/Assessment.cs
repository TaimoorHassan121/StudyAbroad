using System;
using System.ComponentModel.DataAnnotations;

namespace Study_Abroad.Models
{
    public class Assessment
    {
        [Key]
        public long AssessmentId { get; set; }
        public string CountryOfInterest { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string LanguageCertification { get; set; }
        public string CourseOfInterest { get; set; }
        public string UniversityOfInterest { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();
    }
}
