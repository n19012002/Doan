using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Configuration;

namespace Doan.Controllers
{
    public class BlogController : Controller
    {
        private readonly HarmicContext _context;

        public BlogController(HarmicContext context)
        {
            _context = context;
        }

		public IActionResult Index(int? page, string searchString)
		{
			ViewBag.Keyword = searchString;
			if (page == null) page = 1;
			int pageSize = 4;

			var blogsQuery = _context.TbBlogs.Where(i => (bool)i.IsActive);
	
			if (!string.IsNullOrEmpty(searchString))
			{
				searchString = searchString.ToLower();
				blogsQuery = blogsQuery.Where(b => b.Title.ToLower().Contains(searchString));
			}

			// Sắp xếp theo BlogId giảm dần
			var blogs = blogsQuery.OrderByDescending(i => i.BlogId).ToPagedList((int)page, pageSize);

			ViewBag.blogComment = _context.TbBlogComments.ToList();

			return View(blogs);
		}


		[Route("/blog/{alias}-{id}.html")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _context.TbBlogs.Where(i => i.BlogId == id && (bool)i.IsActive).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }
            ViewBag.blogComment = _context.TbBlogComments.Where(i => i.BlogId == id && (bool)i.IsActive).OrderByDescending(i => i.CommentId).ToList();
            return View(post);
        }
        [HttpPost]
        public IActionResult Create(int? id, string name, string phone, string email, string message)
        {
            try
            {
                TbBlogComment comment = new TbBlogComment();
                comment.BlogId = id;
                comment.Name = name;
                comment.Phone = phone;
                comment.Email = email;
                comment.Detail = message;
                comment.CreatedDate = DateTime.Now;
                _context.Add(comment);
                _context.SaveChangesAsync();
                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }
    }
}