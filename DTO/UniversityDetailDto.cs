using Study_Abroad.Models;
using Study_Abroad.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.DTO
{
    public class UniversityDetailDto
    {
        public University University { get; set; }
        public List<Course> Courses { get; set; }
    
    }
}
