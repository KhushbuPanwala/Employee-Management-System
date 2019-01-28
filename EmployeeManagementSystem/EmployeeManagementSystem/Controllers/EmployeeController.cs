using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var context = new EFCoreWebDemoContext();
            using (var context = new EmployeeManagementContext())
            {
                var model = await context.Employees.Include(a => a.Department).AsNoTracking().ToListAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            using (var _context = new EmployeeManagementContext())
            {
                var departments = await _context.Departments.Select(a => new SelectListItem
                {
                    Value = a.DeptId.ToString(),
                    Text = $"{a.DeptName}"
                }).ToListAsync();
                ViewBag.Departments = departments;

                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Address,Qualification,ContactNumber, DeptId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var _context = new EmployeeManagementContext())
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            using (var _context = new EmployeeManagementContext())
            {
                var departments = await _context.Departments.Select(a => new SelectListItem
                {
                    Value = a.DeptId.ToString(),
                    Text = $"{a.DeptName}"
                }).ToListAsync();
                ViewBag.Departments = departments;
                //ViewData["DeptId"] = new SelectList(_context.Departments, "DeptId", "DeptId", employee.DeptId);
            }
            return View(employee);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var _context = new EmployeeManagementContext())
            {
                var employee = await _context.Employees
                .Include(b => b.Department)
                .FirstOrDefaultAsync(m => m.EmpId == id);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
        }
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var _context = new EmployeeManagementContext())
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var departments = await _context.Departments.Select(a => new SelectListItem
                {
                    Value = a.DeptId.ToString(),
                    Text = $"{a.DeptName}"
                }).ToListAsync();
                ViewBag.Departments = departments;                
                return View(employee);
            }
        }

        // POST: Employees/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,Name,Surname,Address,Qualification,ContactNumber,DeptId")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var _context = new EmployeeManagementContext())
                    {
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
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
            using (var _context = new EmployeeManagementContext())
            {
                ViewData["DeptId"] = new SelectList(_context.Departments, "DeptId", "DeptId", employee.DeptId);
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var _context = new EmployeeManagementContext())
            {
                var employee = await _context.Employees
                .Include(b => b.Department)
                .FirstOrDefaultAsync(m => m.EmpId == id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var _context = new EmployeeManagementContext())
            {
                var employee = await _context.Employees.FindAsync(id);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool EmployeeExists(int id)
        {
            using (var _context = new EmployeeManagementContext())
            {
                return _context.Employees.Any(e => e.DeptId == id);
            }
        }
    }
}
