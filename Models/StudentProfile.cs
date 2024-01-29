using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class StudentProfile
    {
        [Key]
        public int StudentId { get; set; }
        //Student Info
        public string StudentNum { get; set; }
        public string F_Name { get; set; }
        public string M_Name { get; set; }
        public string L_Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; } 
        [Display(Name ="Country")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country country { get; set; }        
        public int? StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual State state { get; set; }        
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City city { get; set; }
        //Address Detail
        public string Address { get; set; }
        public int? Current_Country { get; set; }
        public int? Postal_Code { get; set; }
        public bool Gender { get; set; }
        public string First_Language { get; set; }
        public bool Marital_Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Passport_Num { get; set; }
        public DateTime? Passport_Expiry { get; set; }
        public string Passport_Pic { get; set; }
        //Education Summary
        public int? Country_of_Education { get; set; }
        public string Education_Level { get; set; }
        public string Grading_Scheme { get; set; }
        public float? Grade_Scale { get; set; }
        public float? Grading_Score { get; set; }
        public bool Graduated_From { get; set; }
        //[ForeignKey("Edu_Id")]
        //public int? Edu_Id { get; set; }
        //public virtual EducationDetail educationDetail { get; set; }
        //Background Information
        public bool IsValid_Visa { get; set; }
        public string IsStudy_Permit { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Referral_Source { get; set; }
        public string Country_Of_Intrest { get; set; }
        public string Service_Of_Intrest { get; set; }
        public bool Confirmation { get; set; }
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();
        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }
        public virtual Agent agent { get; set; }

        //public int? Edu_Id { get; set; }
        //public virtual EducationDetail EducationDetail { get; set; }
    }
}
