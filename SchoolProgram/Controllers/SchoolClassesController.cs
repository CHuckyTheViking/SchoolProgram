using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProgram.Data;
using SchoolProgram.Models;

namespace SchoolProgram.Controllers
{
    [Authorize]
    public class SchoolClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SchoolClassesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.SchoolClass.ToListAsync());
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(string id, SchoolClass schoolClass)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (schoolClass == null)
            {
                return RedirectToAction(nameof(Index));    
            }
            schoolClass = await _context.SchoolClass.Include(s => s.Students).Include(t => t.Teacher)
                .FirstOrDefaultAsync(school => school.Id == id);

            if (schoolClass.Teacher == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (schoolClass.Students == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(schoolClass);
        }

        // GET: SchoolClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClass.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            var _stu = await _userManager.GetUsersInRoleAsync("Student");
            List<AppUser> _students = new List<AppUser>();
            foreach (var s in _stu)
            {
                if (s.inClass == "False")
                {
                    _students.Add(s);
                }
            }

            var viewModel = new EditSchoolClassViewModel()
            {
                CurrentClass = schoolClass,
                Teachers = await _userManager.GetUsersInRoleAsync("Teacher"),
                Students = _students
                
            };

            return View(viewModel);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SchoolClass schoolClass, string Teachers, List<string> Students)
        {

            

            var _teacher = await _context.Users.FirstOrDefaultAsync(AppUser => AppUser.Id == Teachers);
            
            schoolClass.Teacher = _teacher;

            

            foreach (var s in Students)
            {
                var _student = await _context.Users.FirstOrDefaultAsync(AppUser => AppUser.Id == s);
                schoolClass.Students.Add(_student);
            }
            

            if (id != schoolClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var s in schoolClass.Students)
                    {
                        s.inClass = "True";
                        _context.Update(s);
                    }
                    _context.Update(schoolClass);
                    
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
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
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolClass = await _context.SchoolClass.FindAsync(id);
            
            _context.SchoolClass.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(string id)
        {
            return _context.SchoolClass.Any(e => e.Id == id);
        }
    }
}
