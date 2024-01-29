using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        [Display(Name="Country Name")]
        public string CountryName { get; set; }
        [Display(Name = "Upload Image")]
        public string CountryImage { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        //[Display(Name ="Profile Picture")]
        //public string ProfilePicture { get; set; }
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
    }
}
