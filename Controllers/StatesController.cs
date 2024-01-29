using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Services.ReadExcelFile;

namespace Study_Abroad.Controllers
{
    //[Authorize(Roles = "Agent")]
    public class StatesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IReadExcelFileInterface _excelFile;

        public StatesController(StudyAbroadContext context, IReadExcelFileInterface excelFile)
        {
            _context = context;
            _excelFile = excelFile;
        }

        // GET: States
        public async Task<IActionResult> Index()
        {
            var states = await _context.States.Where(a=>a.Status).Include(a => a.Countries).OrderByDescending(a=>a.StateName).ToListAsync();
            return View(states);
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
                var excelStates = await _excelFile.ReadStateExcelFile(file);
                var oldStates = _context.States.ToList();
                var oldCountries = _context.Countries.ToList();
                excelStates = excelStates.Where(a => oldCountries.Any(b => b.CountryId == a.id)).ToList();
                var newExcelStates = excelStates.Where(a => !oldStates.Any(b => b.CountryId == a.id && b.StateName.ToLower() == a.name.ToLower())).Select(a=> new { a.id , a.name}).Distinct().ToList();
                var states = newExcelStates.Select(a => new State { StateName = a.name, CountryId = a.id }).ToList();
                //states.All(a=> { var country = oldCountries.FirstOrDefault(b => b.Country_ID == a.Country_ID); a.Country_Name = country.Country_Name,a.Country_Image = country.Country_Image; return true; });
                await _context.AddRangeAsync(states);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }


        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }
            state.Countries = _context.Countries.Where(s => s.CountryId == state.CountryId).SingleOrDefault();

            return View(state);
        }

        // GET: States/Create
        public IActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(State state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    state.Countries = _context.Countries.Where(s => s.CountryId == state.CountryId).SingleOrDefault();
                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Invalid Value" + "Please Enter the value"+"Go Back and Try Again");
            }
            ViewBag.CountryId = new SelectList(_context.Countries, "CountryId", "CountryName");

            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewBag.CountryId = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateName,Status,DateTime,CountryId")] State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            ViewBag.CountryId = new SelectList(_context.Countries, "CountryId", "CountryName");
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }
            state.Countries = _context.Countries.Where(s => s.CountryId == state.CountryId).SingleOrDefault();

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _context.States.FindAsync(id);
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
