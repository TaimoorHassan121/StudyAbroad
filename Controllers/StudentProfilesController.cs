using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Models.ViewModel;
using Study_Abroad.DTO;
using System.Security.Claims;
using static Study_Abroad.Models.ViewModel.StudentDetails;

namespace Study_Abroad.Controllers
{
    [Authorize(Roles = "Agent, Admin")]
    public class StudentProfilesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly StudyAbroadContext _db;

        public StudentProfilesController(StudyAbroadContext context, StudyAbroadContext db)
        {
            _context = context;
            _db = db;
        }

        // GET: StudentProfiles
        public async Task<IActionResult> Index()
        {
            //int userId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = User.FindFirstValue(ClaimTypes.Role);                

            int AgentId = HttpContext.User.Claims
            .Where(x => x.Type == "AgentId")
            .Select(x => Convert.ToInt32(x.Value))
            .FirstOrDefault();


            StudentDetails student = new StudentDetails();
            var userLogin = _context.Agents.Where(a => a.AgentId == AgentId).SingleOrDefault();
            var ApplicationDetail = _context.ApplicationDetails.Include(a => a.StudentProfile).Include(a => a.University).Include(a => a.Course).ToList();

            if (user == "Admin")
            {
                var studentProf = _context.StudentProfiles.Include(s => s.city).Include(s => s.country).Include(a => a.agent).ToList();
                return View(studentProf);
            }           

            var studentProfile =  _context.StudentProfiles.Include(s => s.city).Include(s => s.country).Include(a=>a.agent)
                .Where(a=>a.AgentId== AgentId).ToList();

            

            foreach(var item in studentProfile)
            {

                ApplicationCount app = new ApplicationCount();

                app.Appcount = ApplicationDetail.Where(a=>a.StudentId==item.StudentId).Count();
                
            }



            //var EDudetail = education;

            return View(studentProfile);


        }

        // GET: StudentProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles
                .Include(s => s.city)
                .Include(s => s.country)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // GET: StudentProfiles/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: StudentProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentProfileVM stdProfile)
        {
            int AgentId = HttpContext.User.Claims
              .Where(x => x.Type == "AgentId")
              .Select(x => Convert.ToInt32(x.Value))
              .FirstOrDefault();
            if (ModelState.IsValid)
            {
                Random r = new Random();
                int myRandomNo = r.Next(10000000, 99999999);
                stdProfile.studentProfile.StudentNum = Convert.ToString(myRandomNo);
                stdProfile.studentProfile.AgentId = AgentId;
                _context.Add(stdProfile.studentProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View(stdProfile);
        }

        // GET: StudentProfiles/Create
        public IActionResult CreateStudent(int? id)
        {

            StudentProfileVM student = new StudentProfileVM();         

            if (id == null)
            {
                return NotFound();
            }
            var studentData = _context.StudentProfiles.Where(a => a.StudentId == id).FirstOrDefault();
            var eduDetail = _context.EducationDetails.Where(a => a.StudentId == id).FirstOrDefault();           
            var languageDb = _context.LanguageTests.Where(a => a.StudentId == id).FirstOrDefault();
            var greTestDb = _context.GreTest.Where(a => a.StudentId == id).FirstOrDefault();
            var gmatDb = _context.GmatTests.Where(a => a.StudentId == id).FirstOrDefault();
            var eduDetailList = _context.EducationDetails.Where(a => a.StudentId == id).ToList();
            student.studentProfile = studentData;
            student.educationDetail = eduDetail;    
            //student.educationList = eduDetailList;
            student.LanguageTest = languageDb;    
            student.GreTests = greTestDb;    
            student.GmatTest = gmatDb;

            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(StudentProfileVM Student, int id)
        {
            if (ModelState.IsValid)
            {                          

                if (Student.studentProfile == null)
                {
                    return BadRequest();
                }
                else
                {
                    Student.LanguageTest.StudentId = id;
                    Student.GreTests.StudentId = id;
                    Student.GmatTest.StudentId = id;
                    Student.studentProfile.StudentId = id;
                    _context.Update(Student.studentProfile);
                    _context.Update(Student.LanguageTest);
                    _context.Update(Student.GreTests);
                    _context.Update(Student.GmatTest);
                    await _context.SaveChangesAsync();
                }
                if (Student.educationDetail == null)
                {
                    return View(Student);
                }
                else
                {
                    if (Student.educationDetail.Edu_Id == 0)
                    {
                        Student.educationDetail.StudentId = id;
                        _context.Add(Student.educationDetail);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Student.educationDetail.StudentId = id;
                        _context.Update(Student.educationDetail);
                        await _context.SaveChangesAsync();
                    }
                }
     
            
                //return RedirectToAction(nameof(Index));
            }

            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["StateId"] = new SelectList(_context.States, "SateId", "StateName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");

            return View(Student);
        }

        [HttpGet]      
        public  IActionResult DetailEducation(int id)
        {
            var EduDetail = _context.EducationDetails.Where(a => a.StudentId == id).ToList();   

            return Ok(EduDetail);
        }

            // GET: StudentProfiles/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles.FindAsync(id);
            if (studentProfile == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", studentProfile.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", studentProfile.CountryId);
            return View(studentProfile);
        }

        // POST: StudentProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentNum,F_Name,M_Name,L_Name,DOB,CountryId,StateId,CityId,Address,Current_Country,Postal_Code,Gender,First_Language,Marital_Status,Email,PhoneNumber,Passport_Num,Passport_Expiry,Passport_Pic,Country_of_Education,Education_Level,Grading_Scheme,Grade_Scale,Grading_Score,Graduated_From,IsValid_Visa,IsStudy_Permit,Comment,Status,Referral_Source,Country_Of_Intrest,Service_Of_Intrest,Confirmation,Date")] StudentProfile studentProfile)
        {
            if (id != studentProfile.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentProfileExists(studentProfile.StudentId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", studentProfile.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", studentProfile.CountryId);
            return View(studentProfile);
        }

        // GET: StudentProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles
                .Include(s => s.city)
                .Include(s => s.country)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // POST: StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentProfile = await _context.StudentProfiles.FindAsync(id);
            _context.StudentProfiles.Remove(studentProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentProfileExists(int id)
        {
            return _context.StudentProfiles.Any(e => e.StudentId == id);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int? id)
        {
            var educationDetail = await _context.EducationDetails.FindAsync(id);
            var stdId = educationDetail.StudentId;
            _context.EducationDetails.Remove(educationDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CreateStudent), new { id = stdId });
        }

        // GET: StudentProfiles/Edit/5
        public async Task<IActionResult> EditEducation(int? id)
        {
            StudentProfileVM studentProfile = new StudentProfileVM(); 


            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.EducationDetails.FindAsync(id);
            studentProfile.educationDetail = education;
            if (education == null)
            {
                return NotFound();
            }   
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            return Ok(studentProfile.educationDetail);
        }
        public IActionResult Search( int id, int countryId, int stateId, int levelId, int intakId, int disiplineId, int tutionStart, int tutionEnd, string course, string university, int school)
        {
            StudentProfileVM student = new StudentProfileVM();
            var courses = _db.Courses.Where(a => a.Status).Include(a => a.Campuses).Include(a=>a.ProgramLevels).ToList();
            var uni = _db.Universities.Where(a => a.Status).Include(a => a.States).Include(a => a.Countries).ToList();
            var std = _db.StudentProfiles.Where(a => a.StudentId == id).SingleOrDefault();
            var lang = _db.LanguageTests.Where(a => a.StudentId == id).SingleOrDefault();
            var Gre = _db.GreTest.Where(a => a.StudentId == id).SingleOrDefault();
            var Gmat = _db.GmatTests.Where(a => a.StudentId == id).SingleOrDefault();
            var intak = _context.CourseIntakes.Where(a => a.CourseIntakeId == intakId).SingleOrDefault();
            tutionStart = tutionStart * 1000;
            tutionEnd = tutionEnd * 1000;



            ViewData["Universities"] = uni;
                
                if (!string.IsNullOrEmpty(course))
                uni = uni.Where(a => courses.Any(b => b.CourseName.Contains(course, StringComparison.OrdinalIgnoreCase) && b.Campuses.UniversityId == a.UniversityId)).ToList();
            if (countryId > 0)
                uni = uni.Where(a => a.CountryId == countryId).ToList();
            if (stateId > 0)
                uni = uni.Where(a => a.StateId == stateId).ToList();
            if (school > 0)
                uni = uni.Where(a => a.UniversityId == school).ToList();
            if (levelId > 0)
                uni = uni.Where(a => courses.Any(b => b.Program_Id == levelId  && b.Campuses.UniversityId == a.UniversityId)).ToList();
            if (intakId > 0)
                uni = uni.Where(a => courses.Any(b => b.CourseIntake == intak.CourseIntakeValue && b.Campuses.UniversityId == a.UniversityId)).ToList();
            if (disiplineId > 0)
                uni = uni.Where(a => courses.Any(b => b.DisiplineId == disiplineId && b.Campuses.UniversityId == a.UniversityId)).ToList();
           
    


            if (tutionStart > 0)



            uni = uni.Where(a => courses.Any(b =>
                {
                    string courseTutionFee = b.CourseTutionFee.Split()[1];
                    int TutionFee = int.Parse(courseTutionFee.Replace(",", ""));
                    int courseTutionFeeValue;
               
                    if (int.TryParse(courseTutionFee.Replace(",", ""), out courseTutionFeeValue))
                    {
                        return courseTutionFeeValue >= tutionStart && courseTutionFeeValue <= tutionEnd && b.Campuses.UniversityId == a.UniversityId;
                    }
                    return false;
                })).ToList();
            if (!string.IsNullOrEmpty(university))
                uni = uni.Where(a => a.UniversityName.Contains(university, StringComparison.OrdinalIgnoreCase)).ToList();

            student.SearchUni = uni.Select(a => new UniversityDetailDto
            {
                University = a,
                Courses = courses.Where(c => c.Status && c.Campuses.UniversityId == a.UniversityId).ToList()
            }).ToList();
            student.studentProfile = std;
            student.LanguageTest = lang;
            student.GreTests = Gre;
            student.GmatTest = Gmat;
            ViewData["Courses"] = courses;
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            ViewData["UniversityId"] = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            ViewData["ProgramId"] = new SelectList(_context.ProgramLevels, "Program_Id", "Name");
            ViewData["Intaks"] = new SelectList(_context.CourseIntakes, "CourseIntakeId", "CourseIntakeValue");
            ViewData["DisiplineId"] = new SelectList(_context.Disiplines, "DisiplineId", "DisiplineName");
            return View(student);
        }
        public IActionResult UniversityChange(int id)
        {
            if (id==null)
            {
                return BadRequest();
            }
       
                var University = _context.Universities.Where(a => ((byte)a.SchoolType) == id).ToList();

            var data = new
            {
                University
            };
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> ApplyStudent(int id, int courseId, int UniId)
        {

            if (id == null)
            {
                return NotFound();
            }

            ApplyStudentVM student = new ApplyStudentVM();
            var StudentProfile = _context.StudentProfiles.Where(a => a.StudentId == id).SingleOrDefault();
            var course = _context.Courses.Where(a => a.CourseId == courseId).SingleOrDefault();
            var university = _context.Universities.Where(a => a.UniversityId == UniId).SingleOrDefault();

            student.student = StudentProfile;
            student.course = course;
            student.university = university;

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        public async Task<IActionResult> ApplyCourse(int id, int courseId, int UniId, ApplicationDetails applicationDetails)
        {
   

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else
                {
                    applicationDetails.StudentId = id;
                    applicationDetails.CourseId = courseId;
                    applicationDetails.UniversityId = UniId;
                    _context.Add(applicationDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ApplicationDetail), new { id=id});

                }

            }

            return View(applicationDetails);
        }

        public async Task<IActionResult> ApplicationDetail(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var StDApplyInfo =await _context.ApplicationDetails.Include(a => a.Course).Include(a => a.StudentProfile).Include(a => a.University)
                .Include(a => a.CourseIntake).Where(a => a.StudentId == id).ToListAsync();

            return View(StDApplyInfo);
        }
        public async Task<IActionResult> Application(int id)
        {
           
            var courseDetail = await _context.ApplicationDetails.Include(a => a.Course).Include(a => a.StudentProfile).Include(a => a.University)
                .Include(a => a.CourseIntake).Where(a => a.AppDetailId == id).SingleOrDefaultAsync();
            courseDetail.StudentProfile = _context.StudentProfiles.Where(a => a.StudentId == courseDetail.StudentId).SingleOrDefault();
            courseDetail.StudentProfile.agent = _context.Agents.Where(a => a.AgentId == courseDetail.StudentProfile.AgentId).SingleOrDefault();
            courseDetail.Course.ProgramLevels = _context.ProgramLevels.Where(a => a.Program_Id == courseDetail.Course.Program_Id).FirstOrDefault();

            ViewData["CourseIntake"] = new SelectList(_context.CourseIntakes.Where(a=>a.CourseId== courseDetail.Course.CourseId).ToList(), "CourseIntakeId", "CourseIntakeValue");
            return View(courseDetail);
        }


    }
}
