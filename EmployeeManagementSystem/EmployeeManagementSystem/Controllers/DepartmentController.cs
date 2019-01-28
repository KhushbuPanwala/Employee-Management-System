using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var context = new EmployeeManagementContext())
            {
                var model = await context.Departments.AsNoTracking().ToListAsync();
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptName")] Department department)
        {
            using (var context = new EmployeeManagementContext())
            {
                if (ModelState.IsValid)
                {
                    context.Add(department);
                    await context.SaveChangesAsync();
                    //return RedirectToAction("Index");
                    //return View(author);
                }
                return RedirectToAction("Index");
            }
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var context = new EmployeeManagementContext())
            {
                //var author = await _context.Authors
                var department = await context.Departments
                   .FirstOrDefaultAsync(m => m.DeptId == id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var context = new EmployeeManagementContext())
            {
                var department = await context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }
        
        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeptId,DeptName")] Department department)
        {
            if (id != department.DeptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new EmployeeManagementContext())
                    {
                        context.Update(department);
                        await context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DeptId))
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
            //return View(author);
            return RedirectToAction("Index");
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var context = new EmployeeManagementContext())
            {
                var department = await context.Departments
                .FirstOrDefaultAsync(m => m.DeptId == id);

                if (department == null)
                {
                    return NotFound();
                }
                return View(department);

            }
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var context = new EmployeeManagementContext())
            {
                var author = await context.Departments.FindAsync(id);
                context.Departments.Remove(author);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }

        private bool DepartmentExists(int id)
        {
            using (var context = new EmployeeManagementContext())
            {
                return context.Departments.Any(e => e.DeptId == id);
            }
        }
    }
}