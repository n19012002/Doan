using Microsoft.AspNetCore.Mvc;
using Doan.Models;

namespace Doan.ViewComponents
{
	public class CoursesViewComponent : ViewComponent
	{
		private readonly HarmicContext _context;

		public CoursesViewComponent(HarmicContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			
			var courses = _context.Courses.ToList();

			return View(courses);
		}
	}
}
