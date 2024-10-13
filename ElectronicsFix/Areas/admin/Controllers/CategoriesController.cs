using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicsFix.Areas.admin.Controllers
{
    // Specify that this controller is part of the "admin" area.
    [Area("admin")]
    public class CategoriesController : Controller
    {
        // Field for database context
        private readonly DepiProjectContext _context;

        // Constructor initializes the database context
        public CategoriesController(DepiProjectContext context)
        {
            _context = context;
        }

        // Method to display all categories

        // Asynchronous action to get a list of categories.
        // GET: admin/Categories
        public async Task<IActionResult> Index()
        {
            try
            {
                // Retrieve categories that are not marked for deletion, including their parent categories.
                var categories = await _context.Categories.Include(c => c.ParentCategory).Where(a => a.OnDelete == false).ToListAsync();
                return View(categories); // Return the categories to the Index view.
            }
            catch (Exception ex) // Catch any exceptions that occur during retrieval.
            {
                // Log exception (optional) - this comment suggests logging can be added.
                ModelState.AddModelError(string.Empty, "Error retrieving categories."); // Add an error to the model state for user feedback.
                return View(); // Return the view without categories in case of an error.
            }
        }

        // GET: admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id) // Asynchronous action to get details for a specific category by id.
        {
            if (id == null) return NotFound(); // Return a 404 if the id is null.

            try
            {
                // Retrieve the category including its parent category based on the provided id.
                var category = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .FirstOrDefaultAsync(m => m.CategoryId == id);
                if (category == null) return NotFound(); // Return a 404 if no category is found.

                return View(category); // Return the category to the Details view.
            }
            catch (Exception ex) // Catch any exceptions that occur during retrieval.
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category details."); // Add an error to the model state for user feedback.
                return View(); // Return the view without category details in case of an error.
            }
        }

        // GET: admin/Categories/Create
        public IActionResult Create() // Action to render the create category view.
        {
            try
            {
                // Populate the ParentCategory dropdown list for category selection.
                ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                var category = new Category(); // Create a new instance of Category.
                return View(category); // Return the create view with the new category.
            }
            catch (Exception ex) // Catch any exceptions that occur during view preparation.
            {
                ModelState.AddModelError(string.Empty, "Error preparing the create view."); // Add an error to the model state for user feedback.
                return View(); // Return the view in case of an error.
            }
        }

        // POST: admin/Categories/Create
        [HttpPost] // Specify that this action responds to POST requests.
        [ValidateAntiForgeryToken] // Protect against CSRF attacks.
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CreatedDate,CreatedBy,ImagePath,UpdatedBy,UpdatedDate,ParentCategoryId,OnDelete")] Category category, IFormFile File) // Asynchronous action to create a new category.
        {
            // check image
            if (File != null) category.ImagePath = await ClsUiHelper.UploadImage(File, "Categories");

            // Check if the model state is valid.
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Add(category); // Add the new category to the context.
                    await _context.SaveChangesAsync(); // Save changes to the database asynchronously.
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful creation.
                }
                catch (Exception ex) // Catch any exceptions that occur during saving.
                {
                    ModelState.AddModelError(string.Empty, "Error saving category."); // Add an error to the model state for user feedback.
                }
            }
            // Repopulate the ParentCategory dropdown list in case of validation errors.
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // Asynchronous action to edit a specific category by id.
        // GET: admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Return a 404 if the id is null.
            if (id == null) return NotFound();

            try
            {
                // Retrieve the category based on the provided id.
                var category = await _context.Categories.FindAsync(id);

                // Return a 404 if no category is found.
                if (category == null) return NotFound();

                // Populate the ParentCategory dropdown list for category selection.
                ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
                return View(category); // Return the edit view with the category.
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category for editing."); // Add an error to the model state for user feedback.
                return View(); // Return the view in case of an error.
            }
        }

        // Asynchronous action to update an existing category.
        // POST: admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CreatedDate,CreatedBy,ImageName,UpdatedBy,UpdatedDate,ParentCategoryId,OnDelete")] Category category, IFormFile File)
        {
            // Check if a file was uploaded.
            if (File != null) category.ImagePath = await ClsUiHelper.UploadImage(File, "Categories");

            // Return a 404 if the id does not match the category id.
            if (id != category.CategoryId) return NotFound();

            // Check if the model state is valid.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category); // Update the category in the context.
                    await _context.SaveChangesAsync(); // Save changes to the database asynchronously.
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful update.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId)) return NotFound(); // Return a 404 if the category does not exist.
                    throw; // Rethrow the exception for further handling.
                }
                catch (Exception ex) // Catch any other exceptions that occur during updating.
                {
                    ModelState.AddModelError(string.Empty, "Error updating category."); // Add an error to the model state for user feedback.
                }
            }
            // Repopulate the ParentCategory dropdown list in case of validation errors.
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category); // Return the edit view with the category for corrections.
        }

        // GET: admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id) // Asynchronous action to get a specific category for deletion.
        {
            if (id == null) return NotFound(); // Return a 404 if the id is null.

            try
            {
                // Retrieve the category including its parent category based on the provided id.
                var category = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .FirstOrDefaultAsync(m => m.CategoryId == id);
                if (category == null) return NotFound(); // Return a 404 if no category is found.

                return View(category); // Return the delete confirmation view with the category.
            }
            catch (Exception ex) // Catch any exceptions that occur during retrieval.
            {
                ModelState.AddModelError(string.Empty, "Error retrieving category for deletion."); // Add an error to the model state for user feedback.
                return View(); // Return the view in case of an error.
            }
        }

        // Asynchronous action to confirm deletion of a category.
        // POST: admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Retrieve the category based on the provided id.
                var category = await _context.Categories.FindAsync(id);
                if (category != null) // Check if the category exists.
                {
                    category.UpdatedDate = DateTime.Now; // Update the timestamp before deletion.
                    _context.Update(category); // Update the category in the context.
                    await _context.SaveChangesAsync(); // Save changes to the database asynchronously.
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful deletion.
            }
            catch (Exception ex) // Catch any exceptions that occur during deletion.
            {
                ModelState.AddModelError(string.Empty, "Error deleting category."); // Add an error to the model state for user feedback.
                return View(); // Return the view in case of an error.
            }
        }

        // Private method to check if a category exists by id.
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id); // Return true if the category exists; otherwise, false.
        }
    }
}
