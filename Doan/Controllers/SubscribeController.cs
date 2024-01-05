using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

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

					
					SendThankYouEmail(dzEmail);

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
						   "<h1 style='color: #333;'>Cảm ơn bạn đã đăng ký!</h1>" +
						   "<p style='font-size: 16px; color: #666;'>Chúng tôi rất cảm ơn bạn đã đăng ký nhận thông báo từ chúng tôi.</p>" +
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
}
