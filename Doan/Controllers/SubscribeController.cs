using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Doan.Controllers
{
	public class SubscribeController : Controller
	{
		private readonly HarmicContext _context;

		public SubscribeController(HarmicContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Subscribe(string dzEmail)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(dzEmail))
				{
					// Kiểm tra email
					if (_context.TbSubscribes.Any(s => s.Email == dzEmail))
					{
						return Json(new { status = 0, message = "Email đã tồn tại trong hệ thống." });
					}

					TbSubscribe subscribe = new TbSubscribe
					{
						Email = dzEmail,
						CreateDate = DateTime.Now
					};

					_context.TbSubscribes.Add(subscribe);
					_context.SaveChanges();

					// Trả về JSON với trạng thái thành công
					return Json(new { status = 1, message = "Cảm ơn bạn đã đăng ký!" });
				}

				// Trả về JSON với trạng thái lỗi và thông báo lỗi
				return Json(new { status = 0, message = "Email không hợp lệ." });
			}
			catch (Exception ex)
			{
				// Trả về JSON với trạng thái lỗi và thông báo lỗi
				return Json(new { status = 0, message = $"Đã xảy ra lỗi: {ex.Message}" });
			}
		}


	}
}
