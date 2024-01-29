using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country country { get; set; }
        [ForeignKey("StateId")]
        [Required]
        public int StateId { get; set; }
        public virtual State States { get; set; }
        [Required]
        public string CityName { get; set; }
        public bool Status { get; set; } = true;
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
    }
}
