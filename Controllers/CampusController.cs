using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Services.ReadExcelFile;

namespace Study_Abroad.Controllers
{
    [Authorize(Roles = "Agent")]
    public class CampusController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IReadExcelFileInterface _excelFile;

        public CampusController(StudyAbroadContext context, IReadExcelFileInterface excelFile)
        {
            _context = context;
            this._excelFile = excelFile;
        }

        // GET: Campus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campuses.Include(s => s.Universities).ToListAsync());
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
                var excelCump = await _excelFile.ReadUniCampusExcelFile(file);
                var oldUniversities = _context.Universities.ToList();
                var oldUniCampuses = _context.Campuses.ToList();

                excelCump = excelCump.Where(a => oldUniversities.Any(b => b.UniversityId == a.uniId)).ToList();
                var newExcelCump = excelCump.Where(a => !oldUniCampuses.Any(b => b.UniversityId == a.uniId && b.CampusName.ToLower() == a.campusName.ToLower())).Select(a => new { a.uniId, a.campusName }).Distinct().ToList();
                var campuses = newExcelCump.Select(a => new Campus { CampusName = a.campusName, UniversityId = a.uniId }).ToList();
                //states.All(a=> { var country = oldCountries.FirstOrDefault(b => b.Country_ID == a.Country_ID); a.Country_Name = country.Country_Name,a.Country_Image = country.Country_Image; return true; });
                await _context.AddRangeAsync(campuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }



        // GET: Campus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }
            campus.Universities = _context.Universities.Where(s => s.UniversityId == campus.UniversityId).SingleOrDefault();

            return View(campus);
        }

        // GET: Campus/Create
        public IActionResult Create()
        {
            ViewBag.University_ID = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View();
        }

        // POST: Campus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampusId,CampusName,UniversityId,Status,DateTime")] Campus campus)
        {
            if (ModelState.IsValid)
            {
                campus.Universities = _context.Universities.Where(s => s.UniversityId == campus.UniversityId).SingleOrDefault();
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.University_ID = new SelectList(_context.Universities, "University_ID", "University_Name");
            return View(campus);
        }

        // GET: Campus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            ViewBag.University_ID = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View(campus);
        }

        // POST: Campus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampusId,CampusName,UniversityId,Status,DateTime")] Campus campus)
        {
            if (id != campus.CampusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.CampusId))
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
            ViewBag.University_ID = new SelectList(_context.Universities, "UniversityId", "UniversityName");
            return View(campus);
        }

        // GET: Campus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses
                .FirstOrDefaultAsync(m => m.CampusId == id);
            if (campus == null)
            {
                return NotFound();
            }
            campus.Universities = _context.Universities.Where(s => s.UniversityId == campus.UniversityId).SingleOrDefault();

            return View(campus);
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campus = await _context.Campuses.FindAsync(id);
            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusExists(int id)
        {
            return _context.Campuses.Any(e => e.CampusId == id);
        }
    }
}
