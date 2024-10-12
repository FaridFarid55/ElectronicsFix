﻿namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class EngineersController : Controller
    {
        // Filed
        private readonly DepiProjectContext _context;

        // Constrictor
        public EngineersController(DepiProjectContext context)
        {
            _context = context;
        }


        // Method

        // GET: Engineers
        public async Task<IActionResult> Index()
        {
            try
            {
                var engineers = await _context.Engineers.ToListAsync();
                return View(engineers);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving data.");
                return View();
            }
        }

        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var engineer = await _context.Engineers.FindAsync(id);
                if (engineer == null) return NotFound();
                return View(engineer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving details.");
                return View();
            }
        }

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
            if (!ModelState.IsValid) return View(engineer);

            if (engineer.Password != engineer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            if (_context.Engineers.Any(e => e.Email == engineer.Email))
            {
                ModelState.AddModelError("Email", "Email address is already taken.");
                return View(engineer);
            }

            try
            {
                // Hash the password and confirm password
                engineer.Password = BCrypt.Net.BCrypt.HashPassword(engineer.Password);
                engineer.ConfirmPassword = engineer.Password;

                _context.Add(engineer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the engineer.");
                return View(engineer);
            }
        }

        // GET: Engineers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var engineer = await _context.Engineers.FindAsync(id);
                if (engineer == null) return NotFound();
                return View(engineer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving engineer details.");
                return View();
            }
        }

        // POST: Engineers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EngineerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            if (id != engineer.EngineerId) return NotFound();

            if (!ModelState.IsValid) return View(engineer);

            if (engineer.Password != engineer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            try
            {
                _context.Update(engineer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EngineerExists(engineer.EngineerId))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the engineer.");
                return View(engineer);
            }
        }

        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var engineer = await _context.Engineers.FindAsync(id);
                if (engineer == null) return NotFound();
                return View(engineer);
            }
            catch (Exception ex)
            {
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
                var engineer = await _context.Engineers.FindAsync(id);
                if (engineer != null)
                {
                    _context.Engineers.Remove(engineer);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the engineer.");
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<bool> EngineerExists(int id)
        {
            try
            {
                return await _context.Engineers.AnyAsync(e => e.EngineerId == id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while checking if the engineer exists.");
                return false;
            }
        }

        // Action to check if email already exists (used in Remote validation)
        public JsonResult CheckEmailExists(string email)
        {
            try
            {
                bool emailExists = _context.Engineers.Any(e => e.Email == email);
                return Json(!emailExists); // Returns true if email is unique, false if exists
            }
            catch (Exception ex)
            {
                return Json(false); // Return false in case of an error
            }
        }
    }
}