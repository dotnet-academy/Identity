using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using CrowdFunding.Models;
using CrowdFunding.Core.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CrowdFunding.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly Core.CrowdFundingContext _context;

        public ProjectsController(
            IHostingEnvironment env,
            Core.CrowdFundingContext context)
        {
            _env = env;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.Person)
                .Select(p => p.ToViewModel())
                .ToListAsync();

            return View(projects);
        }

        public async Task<IActionResult> Details(long id)
        {
            var project =await GetProjectAsync(id);

            if (project == null) {
                return NotFound();
            }

            return View(project.ToViewModel());
        }

        [Authorize]
        public IActionResult Create()
        {
            var project = new ProjectViewModel {
                Categories = new SelectList(
                    _context.Category, "CategoryId", "Name")
            };

            return View(project);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel project)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var dbProject = project.ToModel();
            dbProject.PersonId = UserId();

            if (project.Picture.Length > 0) {
                var savePath = Path.Join(
                    _env.WebRootPath, "projects", project.Picture.FileName);

                using (var stream = new FileStream(savePath, FileMode.Create)) {
                    await project.Picture.CopyToAsync(stream);
                }

                dbProject.PictureUrl = $"projects/{project.Picture.FileName}";
            }
                
            _context.Add(dbProject);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var dbProject = await GetProjectAsync(id);
            
            if (dbProject == null) {
                return NotFound();
            }

            var project = dbProject.ToViewModel();

            project.Categories = new SelectList(
                _context.Category, "CategoryId", "Name", project.CategoryId);

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ProjectViewModel project)
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

            project.Categories = new SelectList(
                _context.Category, "CategoryId", "Name", project.CategoryId);

            return View(project);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var project = await GetProjectAsync(id);

            if (project == null) {
                return NotFound();
            }

            return View(project.ToViewModel());
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

        private async Task<Project> GetProjectAsync(long id)
        {
            return await _context.Projects
                .Include(p => p.Category)
                .SingleAsync(p => p.ProjectId == id && p.PersonId == UserId());
        }

        private long UserId() => 
            long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
