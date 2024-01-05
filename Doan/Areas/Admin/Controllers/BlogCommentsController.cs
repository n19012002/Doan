using System;
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
    public class BlogCommentsController : Controller
    {
        private readonly HarmicContext _context;

        public BlogCommentsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogComments
        public async Task<IActionResult> Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Adminlogin");
            var harmicContext = _context.TbBlogComments.Include(t => t.Blog);
            return View(await harmicContext.ToListAsync());
        }

        // GET: Admin/BlogComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogComments == null)
            {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments
                .Include(t => t.Blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (tbBlogComment == null)
            {
                return NotFound();
            }

            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId");
            return View();
        }

        // POST: Admin/BlogComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Name,Phone,Email,CreatedDate,Detail,BlogId,IsActive")] TbBlogComment tbBlogComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbBlogComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbBlogComments == null)
            {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments.FindAsync(id);
            if (tbBlogComment == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // POST: Admin/BlogComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Name,Phone,Email,CreatedDate,Detail,BlogId,IsActive")] TbBlogComment tbBlogComment)
        {
            if (id != tbBlogComment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbBlogComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBlogCommentExists(tbBlogComment.CommentId))
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
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbBlogComments == null)
            {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments
                .Include(t => t.Blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (tbBlogComment == null)
            {
                return NotFound();
            }

            return View(tbBlogComment);
        }

        // POST: Admin/BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbBlogComments == null)
            {
                return Problem("Entity set 'HarmicContext.TbBlogComments'  is null.");
            }
            var tbBlogComment = await _context.TbBlogComments.FindAsync(id);
            if (tbBlogComment != null)
            {
                _context.TbBlogComments.Remove(tbBlogComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbBlogCommentExists(int id)
        {
          return (_context.TbBlogComments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
