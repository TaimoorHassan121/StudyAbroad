using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models
{
    public class Agent
    {
        [Key]
        public int AgentId { get; set; }
        [ForeignKey("IdentityUserId")]
        public string IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public string AgentName { get; set; }
        public string ContactNumber { get; set; }
        [Display(Name = "Email/UserName")]
        public string Agent_Email { get; set; }
        [DataType(DataType.Password)]
        public string Agent_Passward { get; set; }
        public string Address { get; set; }
        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public virtual City Cities { get; set; }
        public string Designation { get; set; }
        public bool Status { get; set; }
        [DisplayFormat(DataFormatString = "0:dd-MMM-yy", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
