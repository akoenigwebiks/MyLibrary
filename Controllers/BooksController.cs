using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.ViewModels;
using MyLibrary.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyLibrary.Controllers
{
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
            var books = await _context.Books
                .Include(b => b.Shelf)
                .ThenInclude(s => s.Library)
                .ToListAsync();

            return View(books);
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
                .ThenInclude(s => s.Library)
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
                LibrariesWithShelves = await GetLibraryWithShelvesAsync()
            };

            return View(viewModel);
        }

        // POST: Books/Create
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

                var shelf = await _context.Shelves
                    .Include(s => s.Books)
                    .FirstOrDefaultAsync(s => s.ShelfId == viewModel.ShelfId);

                if (shelf == null || !shelf.HasEnoughSpace(book))
                {
                    ModelState.AddModelError("", "The selected shelf does not have enough space for the book.");
                    viewModel.LibrariesWithShelves = await GetLibraryWithShelvesAsync();
                    return View(viewModel);
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.LibrariesWithShelves = await GetLibraryWithShelvesAsync();
            return View(viewModel);
        }

        private async Task<List<VMLibraryWithShelves>> GetLibraryWithShelvesAsync()
        {
            var libraries = await _context.Libraries
                .Include(l => l.Shelves)
                .ThenInclude(s => s.Books)
                .ToListAsync();

            return libraries.Select(l => new VMLibraryWithShelves
            {
                LibraryId = l.LibraryId,
                Name = l.Name,
                VMShelfFreeSpaceAndCount = l.Shelves.Select(shelf => new VMShelfFreeSpaceAndCount
                {
                    Shelf = shelf,
                    BookCount = shelf.Books.Count,
                    FreeWidth = shelf.FreeSpace()
                }).ToList()
            }).ToList();
        }
    }
}
