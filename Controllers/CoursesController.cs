using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Services.ReadExcelFile;

namespace Study_Abroad.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IReadExcelFileInterface _excelFile;

        public CoursesController(StudyAbroadContext context, IReadExcelFileInterface excelFile)
        {
            _context = context;
            _excelFile = excelFile;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var studyAbroadContext = await _context.Courses.Include(c => c.Campuses).Include(c => c.Disiplines).Include(c => c.States).Include(c=>c.ProgramLevels).ToListAsync();
            return View(studyAbroadContext);
        }

        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null)
                return View();
            try
            {
                var excelCourse = await _excelFile.ReadCoursesExcelFile(file);
                // var oldCourses = _context.Courses.ToList();
                var oldDisipline = _context.Disiplines.ToList();
                var oldStates = _context.States.ToList();
                var oldCampuses = _context.Campuses.ToList();
                var oldProgram = _context.ProgramLevels.ToList();

                excelCourse = excelCourse.Where(a => oldDisipline.Any(b => b.DisiplineId == a.DisiplineId)
                && oldStates.Any(b => b.StateId == a.StateId)
                && oldCampuses.Any(b => b.CampusId == a.CampusId)
                && oldProgram.Any(b=>b.Program_Id==a.Program_Id)
                ).Distinct().ToList();
                //var newDisiplines = excelDisip.Select(a => new Disipline { DisiplineName = a }).ToList();
                //states.All(a=> { var country = oldCountries.FirstOrDefault(b => b.Country_ID == a.Country_ID); a.Country_Name = country.Country_Name,a.Country_Image = country.Country_Image; return true; });
                await _context.AddRangeAsync(excelCourse);
                await _context.SaveChangesAsync();
                var courseIntake = excelCourse.Where(a => !string.IsNullOrEmpty(a.CourseIntake)).Select(a => new CourseIntake { CourseId = a.CourseId, CourseIntakeValue = a.CourseIntake }).ToList();
                await _context.AddRangeAsync(courseIntake);
                await _context.SaveChangesAsync();
                //return Ok(new { Courses = excelCourse, Intakes = courseIntake });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Campuses)
                .Include(c => c.Disiplines)
                .Include(c => c.States)
                .Include(c => c.ProgramLevels)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["CampusId"] = new SelectList(_context.Campuses, "CampusId", "CampusId");
            ViewData["DisiplineId"] = new SelectList(_context.Disiplines, "DisiplineId", "DisiplineId");
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["Program_Id"] = new SelectList(_context.ProgramLevels, "Program_Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,DisiplineId,CampusId,StateId,CourseName,Program_Id,CourseIntake,CourseMode,CourseTutionFee,CourseDuration,Status,DateTime,CourseScholarship,CourseApplicationDeadline,CourseOverview,CourseLearningOutcome,CourseStartDateAndPrice,CourseHowToApply,CourseReviewAndRanking")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampusId"] = new SelectList(_context.Campuses, "CampusId", "CampusName", course.CampusId);
            ViewData["DisiplineId"] = new SelectList(_context.Disiplines, "DisiplineId", "DisiplineName", course.DisiplineId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", course.StateId);
            ViewData["Program_Id"] = new SelectList(_context.ProgramLevels, "Program_Id", "Name");
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CampusId"] = new SelectList(_context.Campuses, "CampusId", "CampusId", course.CampusId);
            ViewData["DisiplineId"] = new SelectList(_context.Disiplines, "DisiplineId", "DisiplineId", course.DisiplineId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", course.StateId);
            ViewData["Program_Id"] = new SelectList(_context.ProgramLevels, "Program_Id", "Name");
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CourseId,DisiplineId,CampusId,StateId,CourseName,Program_Id,CourseIntake,CourseMode,CourseTutionFee,CourseDuration,Status,DateTime,CourseScholarship,CourseApplicationDeadline,CourseOverview,CourseLearningOutcome,CourseStartDateAndPrice,CourseHowToApply,CourseReviewAndRanking")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampusId"] = new SelectList(_context.Campuses, "CampusId", "CampusId", course.CampusId);
            ViewData["DisiplineId"] = new SelectList(_context.Disiplines, "DisiplineId", "DisiplineId", course.DisiplineId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", course.StateId);
            ViewData["Program_Id"] = new SelectList(_context.ProgramLevels, "Program_Id", "Name");
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Campuses)
                .Include(c => c.Disiplines)
                .Include(c => c.States)
                .Include(c => c.ProgramLevels)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
