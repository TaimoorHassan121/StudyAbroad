using Study_Abroad.Models;
using System.Collections.Generic;

namespace Study_Abroad.DTO
{
    public class AssessmentFormDTO
    {
        public Assessment Assessments { get; set; }
        public List<AssessmentEducationalBackground> AssessmentEducationalBackgrounds { get; set; }
    }
}
