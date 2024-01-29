using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Study_Abroad.Data;
using Study_Abroad.DTO;
using Study_Abroad.Models;
using Study_Abroad.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudyAbroadContext _db;
        private readonly StudyAbroadContext _context;

        public HomeController(ILogger<HomeController> logger, StudyAbroadContext db, StudyAbroadContext context)
        {
            _logger = logger;
            _db = db;
            _context = context;
        }
        //[Authorize(Roles = "Agent, Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            ViewData["NoOfCountries"] = _context.Countries.Where(a => a.Status).Count();
            ViewData["TotalUniversities"] = _context.Universities.Where(a => a.Status).Count();
            ViewData["TotalCourses"] = _context.Courses.Where(a => a.Status).Count();
            ViewData["TotalStudents"] = _context.StudentProfiles.Count();
            ViewData["TotalApplicants"] = _context.Assessments.Where(a => a.Status).Count();
            ViewData["Applicants"] = await _context.Assessments.Where(a => a.Status).ToListAsync();
            return View();
        }

        //public async Task<IActionResult> StudentCreate()
        //{
        //    StudentProfileVM student = new StudentProfileVM();
        //    var stdProfile = _context.StudentProfiles.ToList();

        //    return View();
        //}
        public async Task<IActionResult> Application()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Aboutus()
        {
            return View();
        }

        public IActionResult OurServices()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUs contactUs)
        {
            await _db.AddAsync(contactUs);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult FeaturedUniversities()
        {
            var uni = _db.Universities.Where(a => a.Status).ToList();
            return PartialView("_FeaturedUniversities", uni);
        }
        public IActionResult Program(long id)
        {
            if (id <= 0)
                return NotFound();
            var course = _db.Courses.Include(a=>a.Campuses).ThenInclude(a=>a.Universities).Include(a=>a.States).FirstOrDefault(a => a.CourseId == id);
            if (course == null)
                return NotFound();
            return View(course);
        }
        public IActionResult Search(int countryId, string course, string university)
        {
            var uniSearch = new List<UniversityDetailDto>();
            var courses = _db.Courses.Where(a=>a.Status).Include(a=>a.Campuses).ToList();
            var uni = _db.Universities.Where(a => a.Status).Include(a => a.States).Include(a => a.Countries).ToList();
            ViewData["Universities"] = uni;
            if (!string.IsNullOrEmpty(course))
                uni = uni.Where(a => courses.Any(b => b.CourseName.Contains(course, StringComparison.OrdinalIgnoreCase) && b.Campuses.UniversityId == a.UniversityId)).ToList();
            if (countryId>0)
                uni = uni.Where(a => a.CountryId == countryId).ToList();
            if (!string.IsNullOrEmpty(university))
                uni = uni.Where(a => a.UniversityName.Contains(university, StringComparison.OrdinalIgnoreCase)).ToList();

            uniSearch = uni.Select(a=>new UniversityDetailDto {
                University = a,
                Courses = courses.Where(c=>c.Status && c.Campuses.UniversityId == a.UniversityId).ToList()
            }).ToList();
            ViewData["Courses"] = courses;
            ViewData["Countries"] = new SelectList(_db.Countries.Where(a => a.Status).ToList(), "CountryId", "CountryName"); //_db.Countries.Where(a => a.Status).ToList();
            //ViewData["States"] = new SelectList(_db.States.Where(a => a.Status).ToList(), "StateId", "StateName");
            //ViewData["Campuses"] = new SelectList(_db.Campuses.Where(a => a.Status).ToList(), "CampusId", "CampusName");
            //ViewData["University"] = new SelectList(_db.Universities.Where(a => a.Status).ToList(), "UniversityId", "UniversityName");
            ViewData["Programs"] = new SelectList(_db.Courses.Where(a => a.Status).ToList());
            ViewData["Disciplines"] = new SelectList(_db.Disiplines.Where(a => a.Status).ToList());
            return View(uniSearch);
        }
        public IActionResult University(int id)
        {
            if (id <= 0)
                return NotFound();
            var university = _db.Universities.Include(a => a.States).Include(a => a.Countries).FirstOrDefault(a => a.UniversityId == id);
            if (university == null)
                return NotFound();
            var courses = _db.Courses.Where(a => a.Campuses.UniversityId == id).ToList();
            var data = new UniversityDetailDto()
            {
                Courses = courses,
                University = university
            };


            return View(data);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SearchData(string countryId, string stateId)
        {
            long cId = Convert.ToInt64(countryId);
            long sId = Convert.ToInt64(stateId);
            if(!string.IsNullOrEmpty(countryId) && !string.IsNullOrEmpty(stateId))
                return Json(_context.Universities.Where(a=>a.CountryId == cId &&  a.StateId == sId && a.Status).ToList());
            if (!string.IsNullOrEmpty(countryId))
                return Json(_context.States.Where(a => a.CountryId == cId && a.Status).ToList());
            if (!string.IsNullOrEmpty(stateId))
                return Json(_context.Cities.ToList());
            //return Json(_context.Cities.Where(a => a.StateId == sId && a.Status).ToList());
            return Json(null);
        }

        public IActionResult SearcProgramData(string courseId)
        {
            long csId = Convert.ToInt64(courseId);
            if(!string.IsNullOrEmpty(courseId))
                return Json(_context.CourseIntakes.Where(a=>a.CourseId == csId && a.Status).ToList());
            return Json(null);
        }
        public IActionResult ProgramFilterData(string courseId)
        {
            long csId = Convert.ToInt64(courseId);
            List<CourseIntake> intakes = null;
            if(!string.IsNullOrEmpty(courseId))
                intakes = _context.CourseIntakes.Where(a=>a.CourseId ==  csId).ToList();
            return Json(intakes);
        }

        public IActionResult AssessementForm()
        {
            AssessmentFormDTO model = new AssessmentFormDTO();
            model.AssessmentEducationalBackgrounds = new List<AssessmentEducationalBackground>();
            ViewData["Country"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            ViewData["University"] = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AssessementForm(AssessmentFormDTO assessment)
        {
            if (assessment.Assessments != null)
            {
                assessment.Assessments.Status = true;
                _context.Assessments.Add(assessment.Assessments);
                await _context.SaveChangesAsync();
            }
            if (assessment.AssessmentEducationalBackgrounds!=null)
            {
                foreach(var i in assessment.AssessmentEducationalBackgrounds)
                {
                    i.Status = true;
                    i.AssessmentId = assessment.Assessments.AssessmentId;
                    _context.AssessmentEducationalBackgrounds.Add(i);
                }
                await _context.SaveChangesAsync();
            }
            AssessmentFormDTO model = new AssessmentFormDTO();
            model.AssessmentEducationalBackgrounds = new List<AssessmentEducationalBackground>();
            ViewData["Country"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            ViewData["University"] = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View(model);
        }
    }
}
