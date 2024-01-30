using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PowerBIWeb.Models;
using PowerBIWeb.UI.Web.Models;
using System.Diagnostics;

namespace PowerBIWeb.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<PowerBI> _powerBI;

        public HomeController(ILogger<HomeController> logger,
                              IOptions<PowerBI> powerBI)
        {
            _logger = logger;
            _powerBI = powerBI;
        }

        public IActionResult Index()
        {
            ViewBag.Reports = _powerBI.Value.Workspaces;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
