using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; } = PKDateTimeZone.getDate();

        [ForeignKey("CampusId")]
        public int CampusId { get; set; }
        public virtual Campus Campuses { get; set; }
    }
}
