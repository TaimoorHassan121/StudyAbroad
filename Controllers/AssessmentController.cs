using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using System.Threading.Tasks;

namespace Study_Abroad.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly StudyAbroadContext _context;
        public AssessmentController(StudyAbroadContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.Assessments.ToListAsync());
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var assessment = await _context.Assessments.FindAsync(id);
            if(assessment == null)
            {
                return NotFound();
            }
            _context.Assessments.Remove(assessment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
