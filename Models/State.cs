using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public virtual Country Countries { get; set; }
        [Required]
        [Display(Name ="State Name")]
        public string StateName { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
        //public string Country_Name { get; set; }
        //public string Country_Image { get; set; }
    }
}
