using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ElectronicsFix.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ConsultationsController : Controller
    {
        private readonly DepiProjectContext _context;
        private readonly ILogger<ConsultationsController> _logger;

        public ConsultationsController(DepiProjectContext context, ILogger<ConsultationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Helper method to get CustomerId from email
        private async Task<int?> GetCustomerIdByEmailAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return null;

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            return customer?.CustomerId;
        }

        // GET: Consultations
        public async Task<IActionResult> Index()
        {
            var customerId = await GetCustomerIdByEmailAsync();
            if (customerId == null)
            {
                return View(new List<Consultation>());
            }

            var consultations = _context.Consultations
                .Include(c => c.Customer)
                .Include(c => c.Engineer)
                .Where(c => c.CustomerId == customerId);

            return View(await consultations.ToListAsync());
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerId = await GetCustomerIdByEmailAsync();
            var consultation = await _context.Consultations
                .Include(c => c.Customer)
                .Include(c => c.Engineer)
                .FirstOrDefaultAsync(m => m.ConsultationId == id && m.CustomerId == customerId);

            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultations/Create
        public IActionResult Create()
        {
            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName");

            return View();
        }

        // POST: Consultations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationId,EngineerId,IssueDescription,ConsultationCost")] Consultation consultation)
        {
            var customerId = await GetCustomerIdByEmailAsync();
            if (customerId == null)
            {
                return View(consultation); // or redirect to an error page
            }

            consultation.CustomerId = customerId.Value; // Set CustomerId from email
            consultation.StartDate = DateTime.Now;
            consultation.EndDate = null;
            consultation.Status = "In Progress";

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(consultation);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Consultation created successfully with ID: {Id}", consultation.ConsultationId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the consultation");
                    throw;
                }
            }

            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName", consultation.EngineerId);

            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerId = await GetCustomerIdByEmailAsync();
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null || consultation.CustomerId != customerId)
            {
                return NotFound();
            }

            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName", consultation.EngineerId);

            return View(consultation);
        }

        // POST: Consultations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationId,EngineerId,EndDate,Status,IssueDescription,ConsultationCost")] Consultation consultation)
        {
     
            if (id != consultation.ConsultationId)
            {
                Console.WriteLine("1111111");
                Console.WriteLine(id);
                Console.WriteLine(consultation.ConsultationId);
                return NotFound();
            }

            var customerId = await GetCustomerIdByEmailAsync();
            if (consultation.CustomerId != customerId)
            {
                Console.WriteLine("2222222222");
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    consultation.StartDate = DateTime.Now; // Set StartDate here if needed
                    consultation.Status = "In Progress";

                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.ConsultationId))
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

            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName", consultation.EngineerId);

            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerId = await GetCustomerIdByEmailAsync();
            var consultation = await _context.Consultations
                .Include(c => c.Customer)
                .Include(c => c.Engineer)
                .FirstOrDefaultAsync(m => m.ConsultationId == id && m.CustomerId == customerId);

            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerId = await GetCustomerIdByEmailAsync();
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation != null && consultation.CustomerId == customerId)
            {
                _context.Consultations.Remove(consultation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
            return _context.Consultations.Any(e => e.ConsultationId == id);
        }
    }
}
