using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class EducationDetail
    {
        [Key]
        public int Edu_Id { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public virtual StudentProfile studentProfile { get; set; }
        [ForeignKey("CountryId")]
        public int? Country_Of_Institute { get; set; }
        public virtual Country country { get; set; }
        public string Institute_Name { get; set; }
        public string Education_Level { get; set; }
        public string Degree_Name { get; set; }
        public string Institute_Language { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Degree_Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Degree_End { get; set; }     
        public bool Graduate_From { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Graduation_Date { get; set; }
        public bool Physical_Certificate { get; set; }
        //public int? StateID { get; set; }
        //[ForeignKey("SateId")]
        //public virtual State state { get; set; }        

        public string EDU_Province { get; set; }
        public string EDU_City { get; set; }
        //public int? CityId { get; set; }
        //[ForeignKey("CityId")]
        //public virtual City city { get; set; }
        public string Address { get; set; }
        public int Postal_Code { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();


    }
}
