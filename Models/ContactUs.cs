using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactUsId { get; set; }
        public string ContactUsName { get; set; }
        public string ContactUsEmail { get; set; }
        public string ContactUsMessage { get; set; }
        public string ContactUsSubject { get; set; }
        //public string ContactUsStatus { get; set; }
        //public string ContactUsDesc { get; set; }
        public bool Status { get; set; } = true;
        public DateTime Date { get; set; } = PKDateTimeZone.getDate();
    }
}
