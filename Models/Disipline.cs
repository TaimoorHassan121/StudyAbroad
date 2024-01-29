using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Disipline
    {
        [Key]
        public int DisiplineId { get; set; }
        [Display(Name = "Disipline Name")]
        public string DisiplineName { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();
    }
}
