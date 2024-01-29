using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Study_Abroad.Data;
using Study_Abroad.Models;
using Study_Abroad.Services.ReadExcelFile;

namespace Study_Abroad.Controllers
{
    [Authorize(Roles = "Agent")]
    public class CountriesController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IReadExcelFileInterface _excelFile;
        public CountriesController(StudyAbroadContext context, IWebHostEnvironment env, IReadExcelFileInterface excelFile)
        {
            _context = context;
            _env = env;
            _excelFile = excelFile;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.OrderByDescending(a=>a.CountryName).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        public async Task<IActionResult> Import()
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
                var countryNames = await _excelFile.ReadCountryExcelFile(file);
                var oldCountories = _context.Countries.ToList();
                countryNames = countryNames.Where(a => !oldCountories.Any(b => b.CountryName.ToLower() == a.ToLower())).Distinct().ToList();
                var countries = countryNames.Select(a=>new Country { CountryName = a}).ToList();
                await _context.AddRangeAsync(countries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,CountryName,CountryStatus,DateTime")] Country country, IFormFile Images)
        {
            var Image = Images;
            if (Image != null && Image.Length > 0)
            {
                string rootpath = _env.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                string extention = Path.GetExtension(Image.FileName);
                country.CountryImage = fileName = DateTime.Now.ToString("yymmssfff") + extention;
                string filepath = Path.Combine(rootpath + "/Uploads/", fileName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await Image.CopyToAsync(filestream);
                }
            }
            //var Image = Images;
            //if (Image != null && Image.Length > 0)
            //{
            //    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/");
            //    bool basePathExists = System.IO.Directory.Exists(basePath);
            //    if (!basePathExists) Directory.CreateDirectory(basePath);
            //    var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
            //    var filePath = Path.Combine(basePath, Image.FileName);
            //    var extension = Path.GetExtension(Image.FileName);
            //    if (!System.IO.File.Exists(filePath))
            //    {
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await Image.CopyToAsync(stream);
            //        }
            //    }
            //}

            //if (file != null && file.Length > 0)
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/iamge-content", fileName);
            //    using (var fileSrteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await file.CopyToAsync(fileSrteam);
            //    }
            //}
            var ExitstingCountry = await _context.Countries.SingleOrDefaultAsync(m => m.CountryName == country.CountryName);
            if (ExitstingCountry != null)
            {
                ModelState.AddModelError("", "This Country Already Exits");
                return View(country);
            }
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,CountryName,CountryStatus,DateTime")] Country country, IFormFile Images)
        {
            var Image = Images;
            if (Image != null && Image.Length > 0)
            {
                string rootpath = _env.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                string extention = Path.GetExtension(Image.FileName);
                country.CountryImage = fileName = DateTime.Now.ToString("yymmssfff") + extention;
                string filepath = Path.Combine(rootpath + "/Uploads/", fileName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await Image.CopyToAsync(filestream);
                }
            }
            if (id != country.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryId))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
    }
}
