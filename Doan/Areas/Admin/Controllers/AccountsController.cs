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
    public class AccountsController : Controller
    {
        private readonly HarmicContext _context;

        public AccountsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            var harmicContext = _context.TbAccounts.Include(t => t.Role);
            return View(await harmicContext.ToListAsync());
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbAccounts == null)
            {
                return NotFound();
            }

            var tbAccount = await _context.TbAccounts
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tbAccount == null)
            {
                return NotFound();
            }

            return View(tbAccount);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Username,Password,FullName,Phone,Email,RoleId,LastLogin,IsActive")] TbAccount tbAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbAccount.RoleId);
            return View(tbAccount);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbAccounts == null)
            {
                return NotFound();
            }

            var tbAccount = await _context.TbAccounts.FindAsync(id);
            if (tbAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbAccount.RoleId);
            return View(tbAccount);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Username,Password,FullName,Phone,Email,RoleId,LastLogin,IsActive")] TbAccount tbAccount)
        {
            if (id != tbAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAccountExists(tbAccount.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.TbRoles, "RoleId", "RoleId", tbAccount.RoleId);
            return View(tbAccount);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbAccounts == null)
            {
                return NotFound();
            }

            var tbAccount = await _context.TbAccounts
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (tbAccount == null)
            {
                return NotFound();
            }

            return View(tbAccount);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]

        public async Task<bool> DeleteConfirmed(int id)
        {
            try
            {
                var tbBlog = await _context.TbAccounts.FindAsync(id);
                if (tbBlog != null)
                {
                    _context.TbAccounts.Remove(tbBlog);
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
            var blog = _context.TbAccounts.Find(id);

            if (blog != null)
            {
                // Chuyển đổi trạng thái
                blog.IsActive = !blog.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }

        private bool TbAccountExists(int id)
        {
          return (_context.TbAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
