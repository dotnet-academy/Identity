using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrowdFunding.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CrowdFunding.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly CrowdFundingContext _context;

        public ProjectsController(CrowdFundingContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Person)
                .ToListAsync();

            return View(projects);
        }

        public async Task<IActionResult> Details(long id)
        {
            var project =await GetProjectAsync(id);

            if (project == null) {
                return NotFound();
            }

            return View(project);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(
                _context.Category, "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
                
            project.PersonId = UserId();
            _context.Add(project);

            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Edit(long id)
        {
            var project = await GetProjectAsync(id);
            
            if (project == null) {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(
                _context.Category, "CategoryId", "Name", project.CategoryId);

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Project project)
        {
            var dbProject = await GetProjectAsync(id);

            if (dbProject == null) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                
                dbProject.Goal = project.Goal;
                dbProject.Title = project.Title;
                dbProject.Deadline = project.Deadline;
                dbProject.CategoryId = project.CategoryId;
                dbProject.PictureUrl = project.PictureUrl;
                dbProject.Description = project.Description;

                _context.Update(dbProject);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(
                _context.Category, "CategoryId", "Name", project.CategoryId);

            return View(project);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var project = await GetProjectAsync(id);

            if (project == null) {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var project = await GetProjectAsync(id);

            if (project == null) {
                return NotFound();
            }

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<Project> GetProjectAsync(long id) =>
            _context.Projects
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProjectId == id &&
                    p.PersonId == UserId());

        private long UserId() => 
            long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
