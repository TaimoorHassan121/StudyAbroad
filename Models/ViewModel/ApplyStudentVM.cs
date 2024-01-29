using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models.ViewModel
{
    public class ApplyStudentVM
    {
        public StudentProfile student { get; set; }
        public Course course { get; set; }
        public University university { get; set; }
        public ApplicationDetails applicationDetails { get; set; }
    }
}
