using AssignmentTest1.Data;
using AssignmentTest1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentTest1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FacilityAdminController : Controller
    {
        private readonly FitBookDbContext _context;

        public FacilityAdminController(FitBookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var facilities = await _context.Facilities
                .OrderBy(f => f.Category)
                .ThenBy(f => f.Name)
                .ToListAsync();

            return View(facilities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Facility());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Facility facility)
        {
            if (!ModelState.IsValid)
                return View(facility);

            facility.CreatedAt = DateTime.Now;
            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Facility added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
                return NotFound();

            return View(facility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Facility facility)
        {
            if (id != facility.FacilityId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(facility);

            var existing = await _context.Facilities.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = facility.Name;
            existing.Category = facility.Category;
            existing.Description = facility.Description;
            existing.Capacity = facility.Capacity;
            existing.HourlyRate = facility.HourlyRate;
            existing.IsActive = facility.IsActive;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Facility updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
                return NotFound();

            facility.IsActive = !facility.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = facility.IsActive
                ? "Facility activated."
                : "Facility deactivated.";

            return RedirectToAction(nameof(Index));
        }
    }
}