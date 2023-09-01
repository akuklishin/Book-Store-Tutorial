using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System.Diagnostics;

namespace Book_Store.Areas.Customer.Controllers
{
    //The above path was automaticly adjusted by pressing "OK" in the pop up window when moving file to area
    //But there is one more option to guide to the area:
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
