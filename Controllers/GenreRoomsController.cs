using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class GenreRoomsController : Controller
    {
        private readonly MyLibraryContext _context;

        public GenreRoomsController(MyLibraryContext context)
        {
            _context = context;
        }

        // GET: GenreRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.GenreRoom.ToListAsync());
        }

        // GET: GenreRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreRoom = await _context.GenreRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreRoom == null)
            {
                return NotFound();
            }

            return View(genreRoom);
        }

        // GET: GenreRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenreRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GenreRoom genreRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genreRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genreRoom);
        }

        // GET: GenreRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreRoom = await _context.GenreRoom.FindAsync(id);
            if (genreRoom == null)
            {
                return NotFound();
            }
            return View(genreRoom);
        }

        // POST: GenreRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GenreRoom genreRoom)
        {
            if (id != genreRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genreRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreRoomExists(genreRoom.Id))
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
            return View(genreRoom);
        }

        // GET: GenreRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreRoom = await _context.GenreRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreRoom == null)
            {
                return NotFound();
            }

            return View(genreRoom);
        }

        // POST: GenreRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreRoom = await _context.GenreRoom.FindAsync(id);
            if (genreRoom != null)
            {
                _context.GenreRoom.Remove(genreRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreRoomExists(int id)
        {
            return _context.GenreRoom.Any(e => e.Id == id);
        }
    }
}
