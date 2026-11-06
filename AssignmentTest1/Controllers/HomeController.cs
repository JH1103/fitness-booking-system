using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using AssignmentTest1.Models.ViewModels;

namespace AssignmentTest1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "Admin")
                    return RedirectToAction("Index", "Dashboard");

                if (role == "Trainer")
                    return RedirectToAction("TrainerDashboard", "Dashboard");

                if (role == "Member" || role == "Receptionist")
                    return RedirectToAction("Index", "Facilities");
            }

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