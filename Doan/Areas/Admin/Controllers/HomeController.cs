using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

      
    }
}