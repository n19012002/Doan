using Doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doan.ViewComponents
{
	public class SliderViewComponent : ViewComponent
	{
		private readonly HarmicContext _context;

		public SliderViewComponent(HarmicContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _context.Sliders.Where(m => (bool)m.IsActive).OrderByDescending(i => i.Sliderid).Take(5).ToList();

			return await Task.FromResult<IViewComponentResult>(View(items));
		}
	}
}
