
namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class EngineersController : Controller
    {
        private readonly DepiProjectContext _context;

        public EngineersController(DepiProjectContext context)
        {
            _context = context;
        }

        // GET: Engineers
        public async Task<IActionResult> Index()
        {
            var engineers = await _context.Engineers.ToListAsync();
            return View(engineers);
        }

        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineers
                .FirstOrDefaultAsync(m => m.EngineerId == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }

        // GET: Engineers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Engineers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EngineerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            // Check if passwords match
            if (engineer.Password != engineer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            // Check if email already exists in the database
            if (_context.Engineers.Any(e => e.Email == engineer.Email))
            {
                ModelState.AddModelError("Email", "Email address is already taken.");
                return View(engineer);
            }

            // Hash the password and confirm password
            engineer.Password = BCrypt.Net.BCrypt.HashPassword(engineer.Password);
            engineer.ConfirmPassword = engineer.Password; // Assign the hashed password to ConfirmPassword as well

            if (ModelState.IsValid)
            {
                _context.Add(engineer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(engineer);
        }


        // GET: Engineers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineers.FindAsync(id);
            if (engineer == null)
            {
                return NotFound();
            }
            return View(engineer);
        }

        // POST: Engineers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EngineerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            if (id != engineer.EngineerId)
            {
                return NotFound();
            }

            if (engineer.Password != engineer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engineer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EngineerExists(engineer.EngineerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(engineer);
        }

        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineers
                .FirstOrDefaultAsync(m => m.EngineerId == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }

        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var engineer = await _context.Engineers.FindAsync(id);
            if (engineer != null)
            {
                _context.Engineers.Remove(engineer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EngineerExists(int id)
        {
            return await _context.Engineers.AnyAsync(e => e.EngineerId == id);
        }

        // Action to check if email already exists (used in Remote validation)
        public IActionResult CheckEmailExists(string email)
        {
            var emailExists = _context.Engineers.Any(e => e.Email == email);
            return Json(!emailExists); // Returns true if email is unique, false if exists
        }
    }
}
