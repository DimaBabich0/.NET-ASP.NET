using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HW03.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW03.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public MoviesController(MovieContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: MoviesController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: MoviesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: MoviesController/Create
        //public IActionResult Create()
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DirectorsName,Genre,PosterPath,Year,Synopsis")] Movie movie, IFormFile? uploadedFile)
        {
            Console.WriteLine("URL: " + movie.PosterPath);
            Console.WriteLine("File: " + uploadedFile);

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string filePath = "/Files/" + uploadedFile.FileName;
                    string fullPath = Path.Combine(_appEnvironment.WebRootPath, "Files", uploadedFile.FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }

                    movie.PosterPath = filePath;
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: MoviesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DirectorsName,Genre,Year,PosterPath,Synopsis")] Movie movie, IFormFile? uploadedFile)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        string filePath = "/Files/" + uploadedFile.FileName;
                        string fullPath = Path.Combine(_appEnvironment.WebRootPath, "Files", uploadedFile.FileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(stream);
                        }

                        if (!string.IsNullOrEmpty(movie.PosterPath))
                        {
                            string oldFile = Path.Combine(_appEnvironment.WebRootPath, movie.PosterPath.TrimStart('/'));
                            if (System.IO.File.Exists(oldFile))
                            {
                                System.IO.File.Delete(oldFile);
                            }
                        }

                        movie.PosterPath = filePath;
                    }

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Movies.Any(e => e.Id == id))
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
            return View(movie);
        }

        // GET: MoviesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
