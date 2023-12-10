using Doan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doan.ViewComponents
{
    public class EventViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public EventViewComponent(HarmicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.Events.Where(m => (bool)m.IsActive).OrderByDescending(i => i.EventId).Take(5).ToList();
            
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
