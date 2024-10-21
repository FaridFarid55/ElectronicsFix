namespace ElectronicsFix.Controllers
{
    public class EngineerTaskController : Controller
    {
        private readonly DepiProjectContext _context;
        private readonly ILogger<EngineerTaskController> _logger;

        public EngineerTaskController(DepiProjectContext context, ILogger<EngineerTaskController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Engineers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Engineers.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Engineers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EngineerId,FirstName,LastName,Specialization,PhoneNumber")] Engineer engineer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(engineer);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Engineer created successfully with ID: {Id}", engineer.EngineerId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the engineer");
                    throw;
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("EngineerId,FirstName,LastName,Specialization,PhoneNumber")] Engineer engineer)
        {
            if (id != engineer.EngineerId)
            {
                return NotFound();
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
                    if (!EngineerExists(engineer.EngineerId))
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

        private bool EngineerExists(int id)
        {
            return _context.Engineers.Any(e => e.EngineerId == id);
        }
    }
}
