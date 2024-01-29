using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Campus
    {
        [Key]
        public int CampusId { get; set; }
        [Display(Name ="Campus Name")]
        public string CampusName { get; set; }
        [ForeignKey("UniversityId")]
        public int UniversityId { get; set; }
        public virtual University Universities { get; set; }
        //[ForeignKey("CityId")]
        //public int CityId { get; set; }
        //public virtual City city { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;

        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();


    }
}
