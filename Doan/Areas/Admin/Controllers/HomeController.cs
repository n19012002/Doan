using Doan.Models;
using Doan.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Adminlogin");
            return View();
        }

      
    }
}