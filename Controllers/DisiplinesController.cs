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
    public class DisiplinesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IReadExcelFileInterface _excelFile;

        public DisiplinesController(StudyAbroadContext context, IReadExcelFileInterface excelFile)
        {
            _context = context;
            this._excelFile = excelFile;
        }

        // GET: Disiplines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disiplines.ToListAsync());
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
                var excelDisip = await _excelFile.ReadDisiplineExcelFile(file);
                var oldDisipline= _context.Disiplines.ToList();

                excelDisip = excelDisip.Where(a => !oldDisipline.Any(b => b.DisiplineName.ToLower().Trim() == a.ToLower().Trim()) ).Distinct().ToList();
                var newDisiplines = excelDisip.Select(a => new Disipline { DisiplineName = a }).ToList();
                //states.All(a=> { var country = oldCountries.FirstOrDefault(b => b.Country_ID == a.Country_ID); a.Country_Name = country.Country_Name,a.Country_Image = country.Country_Image; return true; });
                await _context.AddRangeAsync(newDisiplines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }



        // GET: Disiplines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disipline = await _context.Disiplines
                .FirstOrDefaultAsync(m => m.DisiplineId == id);
            if (disipline == null)
            {
                return NotFound();
            }

            return View(disipline);
        }

        // GET: Disiplines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disiplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisiplineId,DisiplineName,Status,DateTime")] Disipline disipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disipline);
        }

        // GET: Disiplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disipline = await _context.Disiplines.FindAsync(id);
            if (disipline == null)
            {
                return NotFound();
            }
            return View(disipline);
        }

        // POST: Disiplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisiplineId,DisiplineName,Status,DateTime")] Disipline disipline)
        {
            if (id != disipline.DisiplineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisiplineExists(disipline.DisiplineId))
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
            return View(disipline);
        }

        // GET: Disiplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disipline = await _context.Disiplines
                .FirstOrDefaultAsync(m => m.DisiplineId == id);
            if (disipline == null)
            {
                return NotFound();
            }

            return View(disipline);
        }

        // POST: Disiplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disipline = await _context.Disiplines.FindAsync(id);
            _context.Disiplines.Remove(disipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisiplineExists(int id)
        {
            return _context.Disiplines.Any(e => e.DisiplineId == id);
        }
    }
}
