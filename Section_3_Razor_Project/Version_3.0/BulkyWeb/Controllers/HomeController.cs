using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Controllers
{
    // HomeController: Controller for managing the default views like Index, Privacy, and Error.
    public class HomeController : Controller
    {
        // Logger instance used to log diagnostic information.
        private readonly ILogger<HomeController> _logger;

        // Constructor that initializes the logger via Dependency Injection.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index action: Returns the main landing page (typically the homepage) of the website.
        public IActionResult Index()
        {
            return View();
        }

        // Privacy action: Returns the Privacy policy page of the website.
        public IActionResult Privacy()
        {
            return View();
        }

        // Error action: Returns a generic error page. Useful for displaying error details in a user-friendly manner.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
