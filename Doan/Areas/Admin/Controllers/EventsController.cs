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
    public class EventsController : Controller
    {
        private readonly HarmicContext _context;

        public EventsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Events
        public async Task<IActionResult> Index()
        {
            var harmicContext = _context.TbEvents.Include(t => t.EventCategory);
            return View(await harmicContext.ToListAsync());
        }

        // GET: Admin/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbEvents == null)
            {
                return NotFound();
            }

            var tbEvent = await _context.TbEvents
                .Include(t => t.EventCategory)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (tbEvent == null)
            {
                return NotFound();
            }

            return View(tbEvent);
        }

        // GET: Admin/Events/Create
        public IActionResult Create()
        {
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryId");
            return View();
        }

        // POST: Admin/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Title,Description,Location,StartDate,EndDate,StartTime,EndTime,Agenda,Speakers,RegistrationLink,ImageUrl,CreatedDate,CreatedBy,ModifiedBy,IsActive,EventCategoryId")] TbEvent tbEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryId", tbEvent.EventCategoryId);
            return View(tbEvent);
        }

        // GET: Admin/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbEvents == null)
            {
                return NotFound();
            }

            var tbEvent = await _context.TbEvents.FindAsync(id);
            if (tbEvent == null)
            {
                return NotFound();
            }
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryId", tbEvent.EventCategoryId);
            return View(tbEvent);
        }

        // POST: Admin/Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Title,Description,Location,StartDate,EndDate,StartTime,EndTime,Agenda,Speakers,RegistrationLink,ImageUrl,CreatedDate,CreatedBy,ModifiedBy,IsActive,EventCategoryId")] TbEvent tbEvent)
        {
            if (id != tbEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEventExists(tbEvent.EventId))
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
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "EventCategoryId", tbEvent.EventCategoryId);
            return View(tbEvent);
        }

        // GET: Admin/Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbEvents == null)
            {
                return NotFound();
            }

            var tbEvent = await _context.TbEvents
                .Include(t => t.EventCategory)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (tbEvent == null)
            {
                return NotFound();
            }

            return View(tbEvent);
        }

        // POST: Admin/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(int id)
        {
            try
            {
                var tbBlog = await _context.TbEvents.FindAsync(id);
                if (tbBlog != null)
                {
                    _context.TbEvents.Remove(tbBlog);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in DeleteConfirmed: {ex.Message}");
                return false;
            }
        }
        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            var blog = _context.TbEvents.Find(id);

            if (blog != null)
            {
                // Chuyển đổi trạng thái
                blog.IsActive = !blog.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }

        private bool TbEventExists(int id)
        {
          return (_context.TbEvents?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
