using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Services.ReadExcelFile;

namespace Study_Abroad.Controllers
{
    [Authorize(Roles = "Agent")]
    public class UniversitiesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IReadExcelFileInterface _excelFile;

        public UniversitiesController(StudyAbroadContext context, IReadExcelFileInterface excelFile)
        {
            _context = context;
            _excelFile = excelFile;
        }

        // GET: Universities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.Include(s => s.States).ThenInclude(m => m.Countries).OrderByDescending(a=>a.UniversityName).ToListAsync());
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
                var excelUni = await _excelFile.ReadUniExcelFile(file);
                var oldStates = _context.States.ToList();
                var oldCountries = _context.Countries.ToList();
                var oldUniversities = _context.Universities.ToList();

                excelUni = excelUni.Where(a => oldCountries.Any(b => b.CountryId == a.countryId)).ToList();
                excelUni = excelUni.Where(a => oldStates.Any(b => b.StateId == a.stateId)).ToList();
                var newExcelUni = excelUni.Where(a => !oldUniversities.Any(b => b.CountryId == a.countryId && b.StateId == a.stateId && b.UniversityName.ToLower() == a.uniName.ToLower())).Select(a => new { a.stateId, a.countryId, a.uniName }).Distinct().ToList();
                var universities = newExcelUni.Select(a => new University { UniversityName = a.uniName, CountryId = a.countryId, StateId = a.stateId }).ToList();
                //states.All(a=> { var country = oldCountries.FirstOrDefault(b => b.Country_ID == a.Country_ID); a.Country_Name = country.Country_Name,a.Country_Image = country.Country_Image; return true; });
                await _context.AddRangeAsync(universities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }


        // GET: Universities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .FirstOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }
            university.States = _context.States.Where(s => s.StateId == university.StateId).SingleOrDefault();
            university.Countries = _context.Countries.Where(m => m.CountryId == university.CountryId).SingleOrDefault();
            return View(university);
        }

                public IActionResult getCountry(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            if (!_context.Countries.Any(a => a.CountryId == id))
                return BadRequest("Invalid Id");

            var state = _context.States.Where(a => a.CountryId == id).ToList();
            //var country = _context.Countries.Where(a => a.CountryId == id).ToList();

            var data = new
            {
                state
                //country
            };

            return Ok(data);
        }

        public IActionResult getCity(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            if (!_context.States.Any(a => a.StateId == id))
                return BadRequest("Invalid Id");

            var City = _context.Cities.Where(a => a.StateId == id).ToList();

            var data = new
            {
                City
            };

            return Ok(data);
        }

        // GET: Universities/Create
        public IActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_context.Countries.OrderByDescending(a=>a.CountryName), "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(_context.States.OrderByDescending(a=>a.StateName), "StateId", "StateName");
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(University university)
        {
            if (ModelState.IsValid)
            {
                university.States = _context.States.Where(s => s.StateId == university.StateId).SingleOrDefault();
                university.Countries = _context.Countries.Where(m => m.CountryId == university.CountryId).SingleOrDefault();
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryId = new SelectList(_context.Countries.OrderByDescending(a => a.CountryName), "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(_context.States.OrderByDescending(a => a.StateName), "StateId", "StateName");
            return View(university);
        }

        // GET: Universities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            ViewBag.CountryId = new SelectList(_context.Countries.OrderByDescending(a => a.CountryName), "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(_context.States.OrderByDescending(a => a.StateName), "StateId", "StateName");
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UniversityId,UniversityName,Status,DateTime,SchoolType,CountryId,StateId")] University university)
        {
            if (id != university.UniversityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.UniversityId))
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
            ViewBag.CountryId = new SelectList(_context.Countries.OrderByDescending(a => a.CountryName), "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(_context.States.OrderByDescending(a => a.StateName), "StateId", "StateName");
            return View(university);
        }

        // GET: Universities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .FirstOrDefaultAsync(m => m.UniversityId == id);
            if (university == null)
            {
                return NotFound();
            }
            university.States = _context.States.Where(s => s.StateId == university.StateId).SingleOrDefault();
            university.Countries = _context.Countries.Where(m => m.CountryId == university.CountryId).SingleOrDefault();
            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var university = await _context.Universities.FindAsync(id);
            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.UniversityId == id);
        }
    }
}
