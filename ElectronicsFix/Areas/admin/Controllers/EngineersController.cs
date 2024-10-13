namespace ElectronicsFix.Areas.admin.Controllers
{
    // Specifies that this controller belongs to the "admin" area
    [Area("admin")]
    public class EngineersController : Controller
    {
        // Field for database context
        private readonly DepiProjectContext _context;

        // Constructor initializes the database context
        public EngineersController(DepiProjectContext context)
        {
            _context = context;
        }

        //This Method Engineers
        #region Method
        // GET: Engineers
        public async Task<IActionResult> Index()
        {
            try
            {
                // Returns the view with the list of all engineers from the database
                return View(await _context.Engineers.ToListAsync());
            }
            catch (Exception ex)
            {
                // Adds an error message to the model in case of failure
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving data.");

                // Returns the view with the error message
                return View();
            }
        }


        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Finds the engineer in the database by ID
                var engineer = await _context.Engineers.FindAsync(id);

                // If the engineer is not found, returns NotFound
                if (engineer == null) return NotFound();

                // Returns the view with the engineer's details
                return View(engineer);
            }
            catch (Exception ex)
            {
                // Adds an error message if details retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving details.");

                // Returns the view with the error message
                return View();
            }
        }

        // Method to render the engineer creation form
        // GET: Engineers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Engineers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EngineerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            // If the form data is invalid, return the view with the submitted data
            if (!ModelState.IsValid) return View(engineer);

            // Check if the passwords match
            if (engineer.Password != engineer.ConfirmPassword)
            {
                // Add an error message if passwords don't match
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            // Check if the email is already taken
            if (_context.Engineers.Any(e => e.Email == engineer.Email))
            {
                // Add an error message if the email is already in use
                ModelState.AddModelError("Email", "Email address is already taken.");
                return View(engineer);
            }

            try
            {
                // Hash the password before saving
                engineer.Password = BCrypt.Net.BCrypt.HashPassword(engineer.Password);
                engineer.ConfirmPassword = engineer.Password;  // Ensure confirm password matches the hashed password

                // Add the new engineer to the database
                _context.Add(engineer);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Adds an error message if the engineer creation fails
                ModelState.AddModelError(string.Empty, "An error occurred while creating the engineer.");
                return View(engineer);
            }
        }

        // GET: Engineers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the engineer by ID
                var engineer = await _context.Engineers.FindAsync(id);

                // Returns NotFound if the engineer is not found
                if (engineer == null) return NotFound();

                // Returns the view with the engineer data for editing
                return View(engineer);
            }
            catch (Exception ex)
            {
                // Adds an error message if retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving engineer details.");
                return View();
            }
        }


        // POST: Engineers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EngineerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            // Returns NotFound if the ID in the URL doesn't match the engineer's ID
            if (id != engineer.EngineerId) return NotFound();

            // If the form data is invalid, return the view with the submitted data
            if (!ModelState.IsValid) return View(engineer);

            // Check if the passwords match
            if (engineer.Password != engineer.ConfirmPassword)
            {
                // Add an error message if passwords don't match
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            try
            {
                // Updates the engineer's details in the database
                _context.Update(engineer);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after a successful edit
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Returns NotFound if the engineer no longer exists
                if (!await EngineerExists(engineer.EngineerId))
                    return NotFound();
                else
                    throw;  // Rethrow the exception if it's not related to concurrency
            }
            catch (Exception ex)
            {
                // Adds an error message if updating fails
                ModelState.AddModelError(string.Empty, "An error occurred while updating the engineer.");
                return View(engineer);
            }
        }


        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the engineer by ID
                var engineer = await _context.Engineers.FindAsync(id);

                // Returns NotFound if the engineer is not found
                if (engineer == null) return NotFound();

                // Returns the view with the engineer data for deletion confirmation
                return View(engineer);
            }
            catch (Exception ex)
            {
                // Adds an error message if retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving engineer details for deletion.");
                return View();
            }
        }


        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Finds the engineer by ID
                var engineer = await _context.Engineers.FindAsync(id);

                // Removes the engineer from the database if found
                if (engineer != null)
                {
                    _context.Engineers.Remove(engineer);
                    await _context.SaveChangesAsync();
                }

                // Redirects to the Index action after deletion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Adds an error message if deletion fails
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the engineer.");
                return RedirectToAction(nameof(Index));
            }
        }

        // Helper method to check if an engineer exists by ID
        private async Task<bool> EngineerExists(int id)
        {
            try
            {
                // Returns true if the engineer exists, false otherwise
                return await _context.Engineers.AnyAsync(e => e.EngineerId == id);
            }
            catch (Exception ex)
            {
                // Adds an error message if checking fails
                ModelState.AddModelError(string.Empty, "An error occurred while checking if the engineer exists.");
                return false;
            }
        }

        // Action to check if email already exists (used in Remote validation)
        public JsonResult CheckEmailExists(string email)
        {
            try
            {
                // Checks if the email already exists in the database
                bool emailExists = _context.Engineers.Any(e => e.Email == email);

                // Returns true if the email is unique, false if it already exists
                return Json(!emailExists);
            }
            catch (Exception ex)
            {
                // Returns false if an error occurs
                return Json(false);
            }
        }
        #endregion
    }
}
