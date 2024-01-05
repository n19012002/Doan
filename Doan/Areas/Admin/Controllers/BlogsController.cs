using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doan.Models;
using X.PagedList;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly HarmicContext _context;

        public BlogsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index(string categoryFilter, string searchTitle, int? page)
        {
            IQueryable<TbBlog> harmicContext = _context.TbBlogs.Include(t => t.Category);

            // Phân trang
            if (page == null) page = 1;
            int pageSize = 4;
         
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                harmicContext = harmicContext.Where(b => b.Category.Title == categoryFilter);
            }
           
            if (!string.IsNullOrEmpty(searchTitle))
            {
                string lowerSearchTitle = searchTitle.ToLower();
                harmicContext = harmicContext.Where(b => EF.Functions.Like(b.Title.ToLower(), $"%{lowerSearchTitle}%"));
            }
           
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.SearchTitle = searchTitle;

           
            ViewBag.AllCategories = await _context.TbCategories.Select(c => c.Title).ToListAsync();
       
            var paginatedBlogs = await harmicContext.AsQueryable().ToPagedListAsync(page ?? 1, pageSize);
            
            ViewBag.PaginatedBlogs = paginatedBlogs;

            return View(paginatedBlogs);
        }



        // GET: Admin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbBlog == null)
            {
                return NotFound();
            }

            return View(tbBlog);
        }

        // GET: Admin/Blogs/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId");
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "Title");
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Alias,CategoryId,Description,Detail,Image,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,AccountId,IsActive")] TbBlog tbBlog)
        {
            if (ModelState.IsValid)
            {
                tbBlog.CreatedDate = DateTime.Now;
                tbBlog.ModifiedDate = DateTime.Now;
                _context.Add(tbBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "Title", tbBlog.CategoryId);
            return View(tbBlog);
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs.FindAsync(id);
            if (tbBlog == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "Title", tbBlog.CategoryId);
            return View(tbBlog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Title,Alias,CategoryId,Description,Detail,Image,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,AccountId,IsActive")] TbBlog tbBlog)
        {
            if (id != tbBlog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					// Thiết lập CreatedDate trước khi cập nhật
					tbBlog.CreatedDate = _context.TbBlogs.AsNoTracking().Where(b => b.BlogId == tbBlog.BlogId).Select(b => b.CreatedDate).FirstOrDefault();
					tbBlog.ModifiedDate = DateTime.Now;
                    _context.Update(tbBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBlogExists(tbBlog.BlogId))
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
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "Title", tbBlog.CategoryId);
            return View(tbBlog);
        }

        // GET: Admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs
                .Include(t => t.Account)
                .Include(t => t.Category)
                
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbBlog == null)
            {
                return NotFound();
            }

            return View(tbBlog);
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(int id)
        {
            try
            {
                var tbBlog = await _context.TbBlogs.FindAsync(id);
                if (tbBlog != null)
                {
                    _context.TbBlogs.Remove(tbBlog);
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
            var blog = _context.TbBlogs.Find(id);

            if (blog != null)
            {
                // Chuyển đổi trạng thái
                blog.IsActive = !blog.IsActive;

                
                _context.SaveChanges();

               
                return Json(true);
            }

           
            return Json(false);
        }

        private bool TbBlogExists(int id)
        {
          return (_context.TbBlogs?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
