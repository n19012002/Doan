
using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

public class ContactController : Controller
{
	private readonly HarmicContext _context;
	private readonly ILogger<ContactController> _logger;

	public ContactController(HarmicContext context, ILogger<ContactController> logger)
	{
		_context = context;
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Contact(TbContact model)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// Kiểm tra
				if (_context.TbContacts.Any(c => c.Email == model.Email))
				{
					return Json(new { status = 0, message = "Email đã tồn tại trong hệ thống." });
				}
		
				model.CreatedDate = DateTime.Now;
	
				_context.TbContacts.Add(model);
				_context.SaveChanges();

				
				return Json(new { status = 1, message = "Cảm ơn bạn đã liên hệ với chúng tôi!" });
			}

			
			return Json(new { status = 0, message = "Dữ liệu không hợp lệ." });
		}
		catch (Exception ex)
		{

			_logger.LogError($"Đã xảy ra lỗi: {ex.Message}");

			return Json(new { status = 0, message = "Đã xảy ra lỗi. Vui lòng thử lại sau." });
		}
	}
}
