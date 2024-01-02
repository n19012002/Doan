using Doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doan.Controllers
{
    public class AboutController : Controller
    {
        private readonly HarmicContext _context;

        public AboutController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           var items = _context.Abouts.Where(m => (bool)m.IsActive).OrderByDescending(i => i.AboutId).ToList();
			return View(items);
        }
    }
}
