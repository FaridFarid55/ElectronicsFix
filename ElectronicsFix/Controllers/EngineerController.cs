using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace ElectronicsFix.Controllers
{
    [Authorize]
    [Route("Engineer")]
    public class EngineerController : Controller
    {
        private readonly DepiProjectContext _context;

        public EngineerController(DepiProjectContext context)
        {
            _context = context;
        }

        // GET: Engineer/Profile
        public IActionResult Profile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var engineer = _context.Engineers.FirstOrDefault(e => e.Email == email);

            if (engineer != null)
            {
                return View("~/Views/Engineer/Profile.cshtml", engineer);
            }

            return NotFound();
        }

        // GET: Engineer/Edit

        [HttpGet("Edit")]
        public IActionResult Edit()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            var engineer = _context.Engineers.FirstOrDefault(e => e.Email == email);

            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Engineer engineer)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingEngineer = await _context.Engineers.FirstOrDefaultAsync(e => e.Email == email);

            if (existingEngineer == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(engineer);
            }

            if (engineer.Password != engineer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(engineer);
            }

            try
            {
                existingEngineer.FirstName = engineer.FirstName;
                existingEngineer.LastName = engineer.LastName;
                existingEngineer.Phone = engineer.Phone;
                existingEngineer.Address = engineer.Address;
                existingEngineer.Password = engineer.Password;
                existingEngineer.ConfirmPassword = engineer.ConfirmPassword;

                _context.Update(existingEngineer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Engineer details updated successfully.";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the engineer.");
                return View(engineer);
            }
        }





        // تعديل دالة التحقق لتتأكد من وجود الـ Engineer وليس Customer
        private async Task<bool> EngineerExists(int id)
        {
            return await _context.Engineers.AnyAsync(e => e.EngineerId == id);
        }




    }
}
