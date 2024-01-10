using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doan.Models;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesBlogController : Controller
    {
        private readonly HarmicContext _context;

        public CategoriesBlogController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoriesBlog
        public async Task<IActionResult> Index()
        {
              return _context.TbCategories != null ? 
                          View(await _context.TbCategories.ToListAsync()) :
                          Problem("Entity set 'HarmicContext.TbCategories'  is null.");
        }

        // GET: Admin/CategoriesBlog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // GET: Admin/CategoriesBlog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoriesBlog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Alias,Description,Position,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbCategory tbCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategory);
        }

        // GET: Admin/CategoriesBlog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory == null)
            {
                return NotFound();
            }
            return View(tbCategory);
        }

        // POST: Admin/CategoriesBlog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Title,Alias,Description,Position,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbCategory tbCategory)
        {
            if (id != tbCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCategoryExists(tbCategory.CategoryId))
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
            return View(tbCategory);
        }

        // GET: Admin/CategoriesBlog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // POST: Admin/CategoriesBlog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbCategories == null)
            {
                return Problem("Entity set 'HarmicContext.TbCategories'  is null.");
            }
            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory != null)
            {
                _context.TbCategories.Remove(tbCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCategoryExists(int id)
        {
          return (_context.TbCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
