using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.ViewModels;

namespace MyLibrary.Controllers;

public class ShelvesController : Controller
{
    private readonly MyLibraryContext _context;

    public ShelvesController(MyLibraryContext context)
    {
        _context = context;
    }

    private int GetRandomShelfId()
    {
        Random random = new Random();
        int randomShelfId;

        do
        {
            randomShelfId = random.Next(1, 1000);
        }
        while (_context.Shelves.Any(s => s.ShelfId == randomShelfId));

        return randomShelfId;
    }


    // GET: Shelves
    public async Task<IActionResult> Index()
    {
        var myLibraryContext = _context.Shelves.Include(s => s.Library);
        return View(await myLibraryContext.ToListAsync());
    }

    // GET: Shelves/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shelf = await _context.Shelves
            .Include(s => s.Library)
            .FirstOrDefaultAsync(m => m.ShelfId == id);
        if (shelf == null)
        {
            return NotFound();
        }

        return View(shelf);
    }

    // GET: Shelves/Create
    public IActionResult Create()
    {
        ViewData["LibraryId"] = new SelectList(_context.Libraries, "LibraryId", "Name");
        return View();
    }

    // POST: Shelves/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LibraryId,Height,Width")] VMAddShelf VMCreateShelf)
    {
        ViewData["LibraryId"] = new SelectList(_context.Libraries, "LibraryId", "Name", VMCreateShelf.LibraryId);
        if (ModelState.IsValid == false) return View(VMCreateShelf);

        try
        {
            Shelf newShelf = new()
            {
                LibraryId = VMCreateShelf.LibraryId,
                Height = VMCreateShelf.Height,
                Width = VMCreateShelf.Width,
                Number = GetRandomShelfId().ToString()
            };
            _context.Add(newShelf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("IX_Shelves_Number") == true)
            {
                ModelState.AddModelError("Number", "Shelf number must be unique.");
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(VMCreateShelf);
        }
    }
}

