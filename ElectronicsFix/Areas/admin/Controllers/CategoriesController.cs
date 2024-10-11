using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        // Filed
        private readonly DepiProjectContext _context;

        // Constrictor
        public CategoriesController(DepiProjectContext context)
        {
            _context = context;
        }

        // Method

        // GET: admin/Categories
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _context.Categories.Include(c => c.ParentCategory).ToListAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                ModelState.AddModelError(string.Empty, "Error retrieving categories.");
                return View();
            }
        }

        // GET: admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .FirstOrDefaultAsync(m => m.CategoryId == id);
                if (category == null) return NotFound();

                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category details.");
                return View();
            }
        }

        // GET: admin/Categories/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error preparing the create view.");
                return View();
            }
        }

        // POST: admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CreatedDate,CreatedBy,ImagePath,UpdatedBy,UpdatedDate,ParentCategoryId,OnDelete")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error saving category.");
                }
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return NotFound();

                ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category for editing.");
                return View();
            }
        }

        // POST: admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CreatedDate,CreatedBy,ImagePath,UpdatedBy,UpdatedDate,ParentCategoryId,OnDelete")] Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId)) return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error updating category.");
                }
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .FirstOrDefaultAsync(m => m.CategoryId == id);
                if (category == null) return NotFound();

                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category for deletion.");
                return View();
            }
        }

        // POST: admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error deleting category.");
                return View();
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
