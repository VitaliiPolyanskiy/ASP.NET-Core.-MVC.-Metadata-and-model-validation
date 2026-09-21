using AspNetCore.Validation.StudentsDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Students_MVC.Controllers;

public class StudentsController(StudentContext context) : Controller
{
    [AcceptVerbs("Get", "Post")]
    public IActionResult CheckEmail(string email)
    {
        // Сучасний синтаксис зіставлення шаблонів замість множинних if
        return Json(email is not ("admin@mail.ua" or "admin@gmail.com"));
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        return context.Students != null ?
                    View(await context.Students.ToListAsync()) :
                    Problem("Набір даних 'StudentContext.Students' порожній.");
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || context.Students == null) return NotFound();

        // Оптимізація: FindAsync замість SingleOrDefaultAsync
        var student = await context.Students.FindAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    // GET: Students/Create
    public IActionResult Create() => View();

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age,GPA,Email")] Student student)
    {
        if (student.Surname?.ToLower() == "admin")
            ModelState.AddModelError(string.Empty, "«admin» — заборонене прізвище.");

        if (student.Name == student.Email)
            ModelState.AddModelError(string.Empty, "Ім'я та електронна адреса не повинні збігатися.");

        if (ModelState.IsValid)
        {
            context.Add(student);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || context.Students == null) return NotFound();

        var student = await context.Students.FindAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Age,GPA,Email")] Student student)
    {
        if (id != student.Id) return NotFound();

        if (student.Surname?.ToLower() == "admin")
            ModelState.AddModelError(string.Empty, "«admin» — заборонене прізвище.");

        if (student.Name == student.Email)
            ModelState.AddModelError(string.Empty, "Ім'я та електронна адреса не повинні збігатися.");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(student);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || context.Students == null) return NotFound();

        var student = await context.Students.FindAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (context.Students == null) return Problem("Набір даних 'StudentContext.Students' порожній.");

        var student = await context.Students.FindAsync(id);
        if (student != null)
        {
            context.Students.Remove(student);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return (context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}