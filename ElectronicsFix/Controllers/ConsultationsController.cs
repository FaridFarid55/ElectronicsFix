using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bl;
using Domains;

namespace ElectronicsFix.Controllers
{
    public class ConsultationsController : Controller
    {
        private readonly DepiProjectContext _context;
        private readonly ILogger<ConsultationsController> _logger;


        public ConsultationsController(DepiProjectContext context, ILogger<ConsultationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Consultations
        public async Task<IActionResult> Index()
        {
            var depiProjectContext = _context.Consultations.Include(c => c.Customer).Include(c => c.Engineer);
            return View(await depiProjectContext.ToListAsync());
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .Include(c => c.Customer)
                .Include(c => c.Engineer)
                .FirstOrDefaultAsync(m => m.ConsultationId == id);
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

            ViewBag.CustomerId = new SelectList(_context.Customers.Select(c => new
            {
                CustomerId = c.CustomerId,
                FullName = c.FirstName + " " + c.LastName
            }), "CustomerId", "FullName");

            return View();
        }

        // POST: Consultations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationId,EngineerId,CustomerId,IssueDescription,ConsultationCost")] Consultation consultation)
        {
            consultation.StartDate = DateTime.Now;
            consultation.EndDate = null;
            consultation.Status = "In Progress";

            if (ModelState.IsValid)
            {
                try
                {
                    // Print the selected EngineerId to the console or logger
                    Console.WriteLine($"Selected Engineer ID: {consultation.EngineerId}");
                    _logger.LogInformation("Selected Engineer ID: {EngineerId}", consultation.EngineerId);

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

            // Re-populate the ViewBag in case of invalid ModelState
            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName", consultation.EngineerId);

            ViewBag.CustomerId = new SelectList(_context.Customers.Select(c => new
            {
                CustomerId = c.CustomerId,
                FullName = c.FirstName + " " + c.LastName
            }), "CustomerId", "FullName", consultation.CustomerId);


            return View(consultation);
        }


        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            // Populate the ViewBag with Engineers and Customers for the dropdowns
            ViewBag.EngineerId = new SelectList(_context.Engineers.Select(e => new
            {
                EngineerId = e.EngineerId,
                FullName = e.FirstName + " " + e.LastName
            }), "EngineerId", "FullName", consultation.EngineerId);

            ViewBag.CustomerId = new SelectList(_context.Customers.Select(c => new
            {
                CustomerId = c.CustomerId,
                FullName = c.FirstName + " " + c.LastName
            }), "CustomerId", "FullName", consultation.CustomerId);


            return View(consultation);
        }

        // POST: Consultations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationId,EngineerId,CustomerId,EndDate,Status,IssueDescription,ConsultationCost")] Consultation consultation)
        {
            if (id != consultation.ConsultationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // You may want to set the StartDate to now or handle it in some other way.
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

            ViewBag.CustomerId = new SelectList(_context.Customers.Select(c => new
            {
                CustomerId = c.CustomerId,
                FullName = c.FirstName + " " + c.LastName
            }), "CustomerId", "FullName", consultation.CustomerId);


            return View(consultation);
        }


        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .Include(c => c.Customer)
                .Include(c => c.Engineer)
                .FirstOrDefaultAsync(m => m.ConsultationId == id);
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
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
            return _context.Consultations.Any(e => e.ConsultationId == id);
        }
    }
}
