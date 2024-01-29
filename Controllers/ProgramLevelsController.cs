using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Data;
using Study_Abroad.Models;

namespace Study_Abroad.Controllers
{
    public class ProgramLevelsController : Controller
    {
        private readonly StudyAbroadContext _context;

        public ProgramLevelsController(StudyAbroadContext context)
        {
            _context = context;
        }

        // GET: ProgramLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgramLevels.ToListAsync());
        }

        // GET: ProgramLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programLevel = await _context.ProgramLevels
                .FirstOrDefaultAsync(m => m.Program_Id == id);
            if (programLevel == null)
            {
                return NotFound();
            }

            return View(programLevel);
        }

        // GET: ProgramLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Program_Id,Name,Date,status")] ProgramLevel programLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programLevel);
        }

        // GET: ProgramLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programLevel = await _context.ProgramLevels.FindAsync(id);
            if (programLevel == null)
            {
                return NotFound();
            }
            return View(programLevel);
        }

        // POST: ProgramLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Program_Id,Name,Date,status")] ProgramLevel programLevel)
        {
            if (id != programLevel.Program_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramLevelExists(programLevel.Program_Id))
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
            return View(programLevel);
        }

        // GET: ProgramLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programLevel = await _context.ProgramLevels
                .FirstOrDefaultAsync(m => m.Program_Id == id);
            if (programLevel == null)
            {
                return NotFound();
            }

            return View(programLevel);
        }

        // POST: ProgramLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programLevel = await _context.ProgramLevels.FindAsync(id);
            _context.ProgramLevels.Remove(programLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramLevelExists(int id)
        {
            return _context.ProgramLevels.Any(e => e.Program_Id == id);
        }
    }
}
