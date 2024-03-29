﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doan.Models;
using Doan.Utilities;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly HarmicContext _context;

        public SlidersController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Sliders
        public async Task<IActionResult> Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Adminlogin");
            return _context.Sliders != null ? 
                          View(await _context.Sliders.ToListAsync()) :
                          Problem("Entity set 'HarmicContext.Sliders'  is null.");
        }

        // GET: Admin/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Sliderid == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Admin/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sliderid,ImageUrl,Title,Subtitle,Description,Link,IsActive")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Admin/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sliderid,ImageUrl,Title,Subtitle,Description,Link,IsActive")] Slider slider)
        {
            if (id != slider.Sliderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Sliderid))
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
            return View(slider);
        }

        // GET: Admin/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Sliderid == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<bool> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Sliders == null)
                {
                    return false;
                }

                var tbMenu = await _context.Sliders.FindAsync(id);

                if (tbMenu != null)
                {
                    _context.Sliders.Remove(tbMenu);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            var blog = _context.Sliders.Find(id);

            if (blog != null)
            {
                // Chuyển đổi trạng thái
                blog.IsActive = !blog.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }

        private bool SliderExists(int id)
        {
          return (_context.Sliders?.Any(e => e.Sliderid == id)).GetValueOrDefault();
        }
    }
}
