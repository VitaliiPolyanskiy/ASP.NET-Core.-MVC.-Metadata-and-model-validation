using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Validation.StudentsDb.Models;

namespace AspNetCore.Validation.StudentsDb.Controllers;

public class BooksController(StudentContext context) : Controller
{
    // GET: Books
    public async Task<IActionResult> Index()
    {
        return context.Books != null ?
                    View(await context.Books.ToListAsync()) :
                    Problem("Набір даних 'StudentContext.Books' порожній.");
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || context.Books == null) return NotFound();

        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();

        return View(book);
    }

    // GET: Books/Create
    public IActionResult Create() => View();

    // POST: Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Author,Year")] Book book)
    {
        if (ModelState.IsValid)
        {
            context.Add(book);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Books/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || context.Books == null) return NotFound();

        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();

        return View(book);
    }

    // POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,Year")] Book book)
    {
        if (id != book.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(book);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || context.Books == null) return NotFound();

        var book = await context.Books.FindAsync(id);
        if (book == null) return NotFound();

        return View(book);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (context.Books == null) return Problem("Набір даних 'StudentContext.Books' порожній.");

        var book = await context.Books.FindAsync(id);
        if (book != null)
        {
            context.Books.Remove(book);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return (context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}