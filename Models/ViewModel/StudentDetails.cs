using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models.ViewModel
{
    public class StudentDetails
    {
        public List<StudentProfile> studentProfile { get; set; }
        public List<EducationDetail> educationDetails { get; set; }
        public ApplicationDetails applicationDetails { get; set; }





        public class ApplicationCount
        {
            public int Appcount { get; set; }
            public StudentProfile student { get; set; }
        }
    }
}
