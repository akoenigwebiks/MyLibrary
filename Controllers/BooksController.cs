using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.ViewModels;

namespace MyLibrary.Controllers;
public class BooksController : Controller
{
    private readonly MyLibraryContext _context;

    public BooksController(MyLibraryContext context)
    {
        _context = context;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        var myLibraryContext = _context.Books
            .Include(b => b.Shelf)
            .ThenInclude(s => s.Library); // Include the related Library entity

        return View(await myLibraryContext.ToListAsync());
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Shelf)
            .ThenInclude(s => s.Library) // Include the related Library entity
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // GET: Books/Create
    public async Task<IActionResult> Create()
    {

        var viewModel = new VMAddBook
        {
            LibrariesWithShelves = GetLibraryWithShelves()
        };

        return View(viewModel);
    }

    // POST: Books/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ShelfId,Title,Height,Thickness")] VMAddBook viewModel)
    {
        ModelState.Remove("LibrariesWithShelves");
        if (ModelState.IsValid)
        {
            var book = new Book
            {
                ShelfId = viewModel.ShelfId,
                Title = viewModel.Title,
                Height = viewModel.Height,
                Thickness = viewModel.Thickness
            };

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.LibrariesWithShelves = GetLibraryWithShelves();
        return View(viewModel);
    }

    private List<LibraryWithShelves> GetLibraryWithShelves()
    {
        return _context.Libraries
            .Include(l => l.Shelves)
            .Select(l => new LibraryWithShelves
            {
                LibraryId = l.LibraryId,
                Name = l.Name,
                Shelves = l.Shelves.ToList()
            })
            .ToList();
    }
}


