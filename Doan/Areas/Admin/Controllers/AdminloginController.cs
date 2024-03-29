﻿using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doan.Areas.Admin.Models;
using Doan.Utilities;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminloginController : Controller
    {
        private readonly HarmicContext _context;
        public AdminloginController(HarmicContext context)
        {
            _context = context;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _context.TbAccounts
        .Include(u => u.Role)
        .FirstOrDefault(u => u.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password) && user.Role != null  )
            {
                if (user.IsActive == false)
                {
                    TempData["ErrorMessage"] = "Tài khoản bị khóa";
                }
                else
                {
                    if(user.Role.RoleName == "User")
                    {
                        TempData["ErrorMessage"] = "Bạn không đủ tuổi vào đây";
                    }
                    else
                    {
                        // Cập nhật thời gian đăng nhập lần cuối
                        user.LastLogin = DateTime.Now;

                        // Lưu các thay đổi vào cơ sở dữ liệu
                        _context.SaveChanges();

                        Functions._AccountId = user.AccountId;
                        Functions._Username = user.FullName;
                        Functions._Email = user.Email;
                        TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    }
                   
                }


            }
            else
            {
                TempData["ErrorMessage"] = "Tên người dùng hoặc mật khẩu không đúng";
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Functions._AccountId = 0;
            Functions._Email = string.Empty;
            Functions._Username = string.Empty;
            return RedirectToAction("Index", "Adminlogin");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetInt32("IsLoggedIn") == 1)
            {
                var username = HttpContext.Session.GetString("UserName1");
                var user = _context.TbAccounts
                    .Include(u => u.Role)
                    .Include(u => u.Profiles)
                    .FirstOrDefault(u => u.Username == username);

                if (user != null && user.Role != null && user.Role.RoleName == "Admin")
                {
                    return View(user);
                }
                else
                {
                    TempData["ErrorMessage"] = "Không có quyền truy cập!";
                    return RedirectToAction("Index", "Login");
                }
            }

            return RedirectToAction("Index", "Login");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TbAccount model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp tên đăng nhập
                if (_context.TbAccounts.Any(u => u.Username == model.Username))
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại!";
                }
                // Kiểm tra trùng lặp email
                else if (_context.TbAccounts.Any(u => u.Email == model.Email))
                {
                    TempData["ErrorMessage"] = "Email đã được sử dụng!";
                }
                // Kiểm tra trùng lặp số điện thoại
                else if (_context.TbAccounts.Any(u => u.Phone == model.Phone))
                {
                    TempData["ErrorMessage"] = "Số điện thoại đã được sử dụng!";
                }
                else
                {
                    var newAccount = new TbAccount
                    {
                        Username = model.Username,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                        FullName = model.FullName,
                        Phone = model.Phone,
                        Email = model.Email,
                        RoleId = 1,
                        IsActive = true
                    };

                    _context.TbAccounts.Add(newAccount);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công!";
                }
            }

            return View("Register");
        }

    }
}
