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
    //public class DepartmentsController : Controller
    //{
    //    private readonly StudyAbroadContext _context;

    //    public DepartmentsController(StudyAbroadContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: Departments
    //    public async Task<IActionResult> Index()
    //    {
    //        return View(await _context.Departments.Include(s=>s.Campuses).ToListAsync());
    //    }

    //    // GET: Departments/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var department = await _context.Departments
    //            .FirstOrDefaultAsync(m => m.Department_ID == id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        department.Campuses = _context.Campuses.Where(s => s.Campus_ID == department.Campus_ID).SingleOrDefault();

    //        return View(department);
    //    }

    //    // GET: Departments/Create
    //    public IActionResult Create()
    //    {
    //        ViewBag.Campus_ID = new SelectList(_context.Campuses, "Campus_ID", "Campus_Name");
    //        return View();
    //    }

    //    // POST: Departments/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(Department department)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            department.Campuses = _context.Campuses.Where(s => s.Campus_ID == department.Campus_ID).SingleOrDefault();
    //            _context.Add(department);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewBag.Campus_ID = new SelectList(_context.Campuses, "Campus_ID", "Campus_Name");

    //        return View(department);
    //    }

    //    // GET: Departments/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var department = await _context.Departments.FindAsync(id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        ViewBag.Campus_ID = new SelectList(_context.Campuses, "Campus_ID", "Campus_Name");

    //        return View(department);
    //    }

    //    // POST: Departments/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Department_ID,Department_Name,Department_Status,DateTime,Campus_ID")] Department department)
    //    {
    //        if (id != department.Department_ID)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(department);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!DepartmentExists(department.Department_ID))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewBag.Campus_ID = new SelectList(_context.Campuses, "Campus_ID", "Campus_Name");

    //        return View(department);
    //    }

    //    // GET: Departments/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var department = await _context.Departments
    //            .FirstOrDefaultAsync(m => m.Department_ID == id);
    //        if (department == null)
    //        {
    //            return NotFound();
    //        }
    //        department.Campuses = _context.Campuses.Where(s => s.Campus_ID == department.Campus_ID).SingleOrDefault();

    //        return View(department);
    //    }

    //    // POST: Departments/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var department = await _context.Departments.FindAsync(id);
    //        _context.Departments.Remove(department);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool DepartmentExists(int id)
    //    {
    //        return _context.Departments.Any(e => e.Department_ID == id);
    //    }
    //}
}
