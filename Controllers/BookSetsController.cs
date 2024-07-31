using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class BookSetsController : Controller
    {
        private readonly MyLibraryContext _context;

        public BookSetsController(MyLibraryContext context)
        {
            _context = context;
        }

        // GET: BookSets
        public async Task<IActionResult> Index()
        {
            var myLibraryContext = _context.BookSet.Include(b => b.GenreRoom).Include(b => b.Shelf);
            return View(await myLibraryContext.ToListAsync());
        }

        // GET: BookSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet
                .Include(b => b.GenreRoom)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSet == null)
            {
                return NotFound();
            }

            return View(bookSet);
        }

        // GET: BookSets/Create
        public IActionResult Create()
        {
            ViewData["GenreRoomId"] = new SelectList(_context.GenreRoom, "Id", "Id");
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id");
            return View();
        }

        // POST: BookSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenreRoomId,ShelfId")] BookSet bookSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreRoomId"] = new SelectList(_context.GenreRoom, "Id", "Id", bookSet.GenreRoomId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", bookSet.ShelfId);
            return View(bookSet);
        }

        // GET: BookSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet.FindAsync(id);
            if (bookSet == null)
            {
                return NotFound();
            }
            ViewData["GenreRoomId"] = new SelectList(_context.GenreRoom, "Id", "Id", bookSet.GenreRoomId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", bookSet.ShelfId);
            return View(bookSet);
        }

        // POST: BookSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenreRoomId,ShelfId")] BookSet bookSet)
        {
            if (id != bookSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookSetExists(bookSet.Id))
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
            ViewData["GenreRoomId"] = new SelectList(_context.GenreRoom, "Id", "Id", bookSet.GenreRoomId);
            ViewData["ShelfId"] = new SelectList(_context.Shelf, "Id", "Id", bookSet.ShelfId);
            return View(bookSet);
        }

        // GET: BookSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSet = await _context.BookSet
                .Include(b => b.GenreRoom)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSet == null)
            {
                return NotFound();
            }

            return View(bookSet);
        }

        // POST: BookSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookSet = await _context.BookSet.FindAsync(id);
            if (bookSet != null)
            {
                _context.BookSet.Remove(bookSet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookSetExists(int id)
        {
            return _context.BookSet.Any(e => e.Id == id);
        }
    }
}
