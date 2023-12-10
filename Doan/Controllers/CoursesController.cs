using Microsoft.AspNetCore.Mvc;
using Doan.Models;
using System.Linq;

public class CoursesController : Controller
{
	private readonly HarmicContext _context;

	public CoursesController(HarmicContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		return View();
	}
}
