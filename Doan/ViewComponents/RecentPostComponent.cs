using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Doan.Models;
using Microsoft.EntityFrameworkCore;

namespace Doan.ViewComponents
{
	[ViewComponent(Name = "RecentPost")]
	public class RecentPostComponent : ViewComponent
	{
		private readonly HarmicContext _connext;
		public RecentPostComponent(HarmicContext connext)
		{
			_connext = connext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofPost = (from p in _connext.TbBlogs
							  where (p.IsActive == true)
							  orderby p.BlogId descending
							  select p).Take(3).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
		}
	}
}
