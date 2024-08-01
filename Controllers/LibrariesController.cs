using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class LibrariesController : Controller
    {
        private readonly MyLibraryContext _context;

        public LibrariesController(MyLibraryContext context)
        {
            _context = context;
        }

        // GET: Libraries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libraries.ToListAsync());
        }

        // GET: Libraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _context.Libraries
                .FirstOrDefaultAsync(m => m.LibraryId == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Library library)
        {
            ModelState.Remove("Shelves");
            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Libraries.Any(l => l.Name == library.Name))
                    {
                        ModelState.AddModelError("Name", "Library name must be unique.");
                        return View(library);
                    }

                    _context.Add(library);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(library);
        }


    }
}
