using System.Security.Claims;

namespace ElectronicsFix.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly DepiProjectContext _context;

        public CustomerController(DepiProjectContext context)
        {
            _context = context;
        }

        // GET: Customer/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            // الحصول على البريد الإلكتروني الخاص بالعميل من الجلسة
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            // البحث عن العميل باستخدام البريد الإلكتروني
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                return View("Profile", customer);
            }

            // إذا لم يتم العثور على العميل
            return NotFound(); // يمكنك تخصيص هذه الرسالة حسب الحاجة
        }


        [HttpGet]
        public IActionResult Edit1()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var customer = _context.Customers.FirstOrDefault(e => e.Email == email);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(Customer customer)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(e => e.Email == email);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            ModelState.Remove("Password");
            //  ModelState.Remove("Logo");
            ModelState.Remove("ConfirmPassword");

            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            // التحقق من أن كلمة المرور قد تم إدخالها في حالة التحديث
            if (!string.IsNullOrWhiteSpace(customer.Password))
            {
                if (customer.Password != customer.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Passwords do not match.");
                    return View(customer);
                }
                existingCustomer.Password = customer.Password;
                existingCustomer.ConfirmPassword = customer.ConfirmPassword;
            }

            try
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Address = customer.Address;

                _context.Update(existingCustomer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Customer details updated successfully.";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the customer.");
                return View(customer);
            }
        }


        private async Task<bool> CustomerExists(int id)
        {
            return await _context.Customers.AnyAsync(e => e.CustomerId == id);
        }
    }

}
