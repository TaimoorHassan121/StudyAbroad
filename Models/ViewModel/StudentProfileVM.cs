using Study_Abroad.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Models.ViewModel
{
    public class StudentProfileVM
    {
        public StudentProfile studentProfile { get; set; }
        public EducationDetail educationDetail { get; set; }
        public GreTest GreTests { get; set; }
        public GmatTest GmatTest { get; set; }
        public LanguageTest LanguageTest { get; set; }

        public University university { get; set; }
        public Course course { get; set; }
        public List<UniversityDetailDto> SearchUni { get; set; }
    }
}
