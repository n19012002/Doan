using Doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doan.Controllers
{
	public class SliderController : Controller
	{
		private readonly HarmicContext _context;

		public SliderController(HarmicContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var items = _context.Sliders.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Sliderid).ToList();
			return View(items);
		}
	}
}
