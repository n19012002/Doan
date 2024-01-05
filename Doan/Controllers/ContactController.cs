
using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;

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
				
				if (_context.TbContacts.Any(c => c.Email == model.Email))
				{
					return Json(new { status = 0, message = "Email đã tồn tại trong hệ thống." });
				}
		
				model.CreatedDate = DateTime.Now;
	
				_context.TbContacts.Add(model);
				_context.SaveChanges();
				
				SendThankYouEmail(model.Email);

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

	private void SendThankYouEmail(string email)
	{
		try
		{
			var smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("daihocsome@gmail.com", "rydknnuckgnwguiy"),
				EnableSsl = true,
			};

			var message = new MailMessage("daihocsome@gmail.com", email)
			{
				Subject = "Cảm ơn bạn đã đăng ký!",
				IsBodyHtml = true, 
				Body = "<html><body style='font-family: Arial, sans-serif;'>" +
					   "<div style='background-color: #f4f4f4; padding: 20px; text-align: center;'>" +
					   "<h1 style='color: #333;'>Cảm ơn bạn đã liên hệ</h1>" +
					   "<p style='font-size: 16px; color: #666;'>Chúng tôi đã nhận được thông tin liên hệ của bạn.Cảm ơn bạn đã liên hệ cho chúng tôi.</p>" +
					   "<img src='https://i.imgur.com/PS2vm5V.jpg' alt='Thank You Image' style='max-width: 100%; border-radius: 10px;'>" +
					   "<div style='margin-top: 20px;'><a href='https://daihoc.somee.com/' style='text-decoration: none; background-color: #4CAF50; color: white; padding: 10px 20px; border-radius: 5px; font-weight: bold;'>Truy cập trang web của chúng tôi</a></div>" +
					   "</div></body></html>"
			};

			smtpClient.Send(message);
		}
		catch (Exception ex)
		{
			
			Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
		}
	}
}
