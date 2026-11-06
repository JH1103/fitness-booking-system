using AssignmentTest1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentTest1.Controllers
{
    public class FacilitiesController : Controller
    {
        private readonly FitBookDbContext _context;

        public FacilitiesController(FitBookDbContext context)
        {
            _context = context;
        }

        // Public page: members and visitors can browse active clubhouse facilities.
        public async Task<IActionResult> Index()
        {
            var facilities = await _context.Facilities
                .AsNoTracking()
                .Where(f => f.IsActive)
                .OrderBy(f => f.Category)
                .ThenBy(f => f.Name)
                .ToListAsync();

            return View(facilities);
        }
    }
}