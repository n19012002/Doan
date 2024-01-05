using Doan.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace Doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Adminlogin");

            return View();
        }
    }
}
