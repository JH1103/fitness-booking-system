using Microsoft.AspNetCore.Mvc;

namespace AssignmentTest1.Controllers
{
    public class FacilitiesController : Controller
    {
        // Public page: members and visitors can browse the clubhouse facilities.
        public IActionResult Index()
        {
            return View();
        }
    }
}