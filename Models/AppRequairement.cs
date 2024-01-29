using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class AppRequairement
    {
        [Key]
        public int ReqId { get; set; }
        public string Passportpic { get; set; }
        public bool Passport_Status { get; set; }
        public string Personal_Statement { get; set; }
        public bool Personal_Status { get; set; }
        public string Privacy_Statement { get; set; }
        public bool Privacy_Status { get; set; }
        public string Addational_Document { get; set; }
        public bool Additional_Status { get; set; }
        public string Bank_Statement { get; set; }
        public bool Bank_Status { get; set; }
        public string Medical_Statement { get; set; }
        public bool Medical_Status { get; set; }
        public string Emergency_Contact { get; set; }
        public string Emergency_Email { get; set; }
        public string Refree_Name { get; set; }
        public string Refree_Conteat { get; set; }
        public string Refree_Email { get; set; }
        public string Refree_Address { get; set; }
    }
}
