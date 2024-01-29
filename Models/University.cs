using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public enum School
    {
        University,
        School,
        College,
        EnglishInstitute,

    }
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public virtual Country Countries { get; set; }
        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public virtual State States { get; set; }
        [Required]
        [Display(Name ="University Name")]
        public string UniversityName { get; set; }
        [Required]
        [Display(Name = "SchoolType")]
        public School SchoolType { get; set; }
        [Display(Name = "University Icon")]
        public string UniversityIcon { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
    }
}
