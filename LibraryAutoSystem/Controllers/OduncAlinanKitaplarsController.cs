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
    public class OduncAlinanKitaplarsController : Controller
    {
        private readonly LibraryContext _context;

        public OduncAlinanKitaplarsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: OduncAlinanKitaplars
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.OduncAlinanKitaplars.Include(o => o.Kitap).Include(o => o.Uye);
            return View(await libraryContext.ToListAsync());
        }

        // GET: OduncAlinanKitaplars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OduncAlinanKitaplars == null)
            {
                return NotFound();
            }

            var oduncAlinanKitaplar = await _context.OduncAlinanKitaplars
                .Include(o => o.Kitap)
                .Include(o => o.Uye)
                .FirstOrDefaultAsync(m => m.KitapId == id);
            if (oduncAlinanKitaplar == null)
            {
                return NotFound();
            }

            return View(oduncAlinanKitaplar);
        }

        // GET: OduncAlinanKitaplars/Create
        public IActionResult Create()
        {
            ViewData["KitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId");
            ViewData["UyeId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId");
            return View();
        }

        // POST: OduncAlinanKitaplars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapId,UyeId,OduncAlmaTarihi")] OduncAlinanKitaplar oduncAlinanKitaplar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oduncAlinanKitaplar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", oduncAlinanKitaplar.KitapId);
            ViewData["UyeId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", oduncAlinanKitaplar.UyeId);
            return View(oduncAlinanKitaplar);
        }

        // GET: OduncAlinanKitaplars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OduncAlinanKitaplars == null)
            {
                return NotFound();
            }

            var oduncAlinanKitaplar = await _context.OduncAlinanKitaplars.FindAsync(id);
            if (oduncAlinanKitaplar == null)
            {
                return NotFound();
            }
            ViewData["KitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", oduncAlinanKitaplar.KitapId);
            ViewData["UyeId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", oduncAlinanKitaplar.UyeId);
            return View(oduncAlinanKitaplar);
        }

        // POST: OduncAlinanKitaplars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapId,UyeId,OduncAlmaTarihi")] OduncAlinanKitaplar oduncAlinanKitaplar)
        {
            if (id != oduncAlinanKitaplar.KitapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oduncAlinanKitaplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OduncAlinanKitaplarExists(oduncAlinanKitaplar.KitapId))
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
            ViewData["KitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", oduncAlinanKitaplar.KitapId);
            ViewData["UyeId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", oduncAlinanKitaplar.UyeId);
            return View(oduncAlinanKitaplar);
        }

        // GET: OduncAlinanKitaplars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OduncAlinanKitaplars == null)
            {
                return NotFound();
            }

            var oduncAlinanKitaplar = await _context.OduncAlinanKitaplars
                .Include(o => o.Kitap)
                .Include(o => o.Uye)
                .FirstOrDefaultAsync(m => m.KitapId == id);
            if (oduncAlinanKitaplar == null)
            {
                return NotFound();
            }

            return View(oduncAlinanKitaplar);
        }

        // POST: OduncAlinanKitaplars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OduncAlinanKitaplars == null)
            {
                return Problem("Entity set 'LibraryContext.OduncAlinanKitaplars'  is null.");
            }
            var oduncAlinanKitaplar = await _context.OduncAlinanKitaplars.FindAsync(id);
            if (oduncAlinanKitaplar != null)
            {
                _context.OduncAlinanKitaplars.Remove(oduncAlinanKitaplar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OduncAlinanKitaplarExists(int id)
        {
          return (_context.OduncAlinanKitaplars?.Any(e => e.KitapId == id)).GetValueOrDefault();
        }
    }
}
