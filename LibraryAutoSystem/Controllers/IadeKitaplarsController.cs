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
    public class IadeKitaplarsController : Controller
    {
        private readonly LibraryContext _context;

        public IadeKitaplarsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: IadeKitaplars
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.IadeKitaplars.Include(i => i.IadeEden).Include(i => i.IadeKitap);
            return View(await libraryContext.ToListAsync());
        }

        // GET: IadeKitaplars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IadeKitaplars == null)
            {
                return NotFound();
            }

            var iadeKitaplar = await _context.IadeKitaplars
                .Include(i => i.IadeEden)
                .Include(i => i.IadeKitap)
                .FirstOrDefaultAsync(m => m.IadeKitapId == id);
            if (iadeKitaplar == null)
            {
                return NotFound();
            }

            return View(iadeKitaplar);
        }

        // GET: IadeKitaplars/Create
        public IActionResult Create()
        {
            ViewData["IadeEdenId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId");
            ViewData["IadeKitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId");
            return View();
        }

        // POST: IadeKitaplars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IadeKitapId,IadeEdenId,IadeTarihi")] IadeKitaplar iadeKitaplar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iadeKitaplar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IadeEdenId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", iadeKitaplar.IadeEdenId);
            ViewData["IadeKitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", iadeKitaplar.IadeKitapId);
            return View(iadeKitaplar);
        }

        // GET: IadeKitaplars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IadeKitaplars == null)
            {
                return NotFound();
            }

            var iadeKitaplar = await _context.IadeKitaplars.FindAsync(id);
            if (iadeKitaplar == null)
            {
                return NotFound();
            }
            ViewData["IadeEdenId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", iadeKitaplar.IadeEdenId);
            ViewData["IadeKitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", iadeKitaplar.IadeKitapId);
            return View(iadeKitaplar);
        }

        // POST: IadeKitaplars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IadeKitapId,IadeEdenId,IadeTarihi")] IadeKitaplar iadeKitaplar)
        {
            if (id != iadeKitaplar.IadeKitapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iadeKitaplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IadeKitaplarExists(iadeKitaplar.IadeKitapId))
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
            ViewData["IadeEdenId"] = new SelectList(_context.Uyelers, "UyeId", "UyeId", iadeKitaplar.IadeEdenId);
            ViewData["IadeKitapId"] = new SelectList(_context.Kitaplars, "KitapId", "KitapId", iadeKitaplar.IadeKitapId);
            return View(iadeKitaplar);
        }

        // GET: IadeKitaplars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IadeKitaplars == null)
            {
                return NotFound();
            }

            var iadeKitaplar = await _context.IadeKitaplars
                .Include(i => i.IadeEden)
                .Include(i => i.IadeKitap)
                .FirstOrDefaultAsync(m => m.IadeKitapId == id);
            if (iadeKitaplar == null)
            {
                return NotFound();
            }

            return View(iadeKitaplar);
        }

        // POST: IadeKitaplars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IadeKitaplars == null)
            {
                return Problem("Entity set 'LibraryContext.IadeKitaplars'  is null.");
            }
            var iadeKitaplar = await _context.IadeKitaplars.FindAsync(id);
            if (iadeKitaplar != null)
            {
                _context.IadeKitaplars.Remove(iadeKitaplar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IadeKitaplarExists(int id)
        {
          return (_context.IadeKitaplars?.Any(e => e.IadeKitapId == id)).GetValueOrDefault();
        }
    }
}
