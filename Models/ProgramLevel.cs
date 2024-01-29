using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class ProgramLevel
    {
        [Key]
        public int Program_Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();
        public bool status { get; set; }
    }
}
