using Doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doan.Controllers
{
	public class EventController : Controller
	{
		private readonly HarmicContext _context;

		public EventController(HarmicContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var items = _context.Events.Where(m => (bool)m.IsActive).OrderByDescending(i => i.EventId).ToList();
			return View(items);
		}
		[Route("/event/{title}-{id}.html")]
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _context.Events.Where(i => i.EventId == id && (bool)i.IsActive).FirstOrDefault();
			if (post == null)
			{
				return NotFound();
			}
			
			return View(post);
		}
	}
}
