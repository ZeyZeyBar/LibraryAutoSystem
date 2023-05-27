using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryAutoSystem.Models;

namespace LibraryAutoSystem.Controllers
{
    public class KitaplarsController : Controller
    {
        private readonly LibraryContext _context;

        public KitaplarsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Kitaplars
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Kitaplars.Include(k => k.Yazar);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Kitaplars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kitaplars == null)
            {
                return NotFound();
            }

            var kitaplar = await _context.Kitaplars
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapId == id);
            if (kitaplar == null)
            {
                return NotFound();
            }

            return View(kitaplar);
        }

        // GET: Kitaplars/Create
        public IActionResult Create()
        {
            ViewData["YazarId"] = new SelectList(_context.Yazars, "YazarId", "YazarId");
            return View();
        }

        // POST: Kitaplars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapId,KitapAdi,YayinEvi,YayinTarihi,Tur,IsbnNo,YazarId")] Kitaplar kitaplar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitaplar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YazarId"] = new SelectList(_context.Yazars, "YazarId", "YazarId", kitaplar.YazarId);
            return View(kitaplar);
        }

        // GET: Kitaplars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kitaplars == null)
            {
                return NotFound();
            }

            var kitaplar = await _context.Kitaplars.FindAsync(id);
            if (kitaplar == null)
            {
                return NotFound();
            }
            ViewData["YazarId"] = new SelectList(_context.Yazars, "YazarId", "YazarId", kitaplar.YazarId);
            return View(kitaplar);
        }

        // POST: Kitaplars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapId,KitapAdi,YayinEvi,YayinTarihi,Tur,IsbnNo,YazarId")] Kitaplar kitaplar)
        {
            if (id != kitaplar.KitapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitaplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitaplarExists(kitaplar.KitapId))
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
            ViewData["YazarId"] = new SelectList(_context.Yazars, "YazarId", "YazarId", kitaplar.YazarId);
            return View(kitaplar);
        }

        // GET: Kitaplars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kitaplars == null)
            {
                return NotFound();
            }

            var kitaplar = await _context.Kitaplars
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapId == id);
            if (kitaplar == null)
            {
                return NotFound();
            }

            return View(kitaplar);
        }

        // POST: Kitaplars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kitaplars == null)
            {
                return Problem("Entity set 'LibraryContext.Kitaplars'  is null.");
            }
            var kitaplar = await _context.Kitaplars.FindAsync(id);
            if (kitaplar != null)
            {
                _context.Kitaplars.Remove(kitaplar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitaplarExists(int id)
        {
          return (_context.Kitaplars?.Any(e => e.KitapId == id)).GetValueOrDefault();
        }
    }
}
