using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ElectronicsFix.Controllers
{
    public class AccountController : Controller
    {
        private readonly DepiProjectContext _context;

        public AccountController(DepiProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and password are required.");
                return View();
            }

            // البحث في جدول المهندسين
            var engineer = _context.Engineers.FirstOrDefault(e => e.Email == email && e.Password == password);

            if (engineer != null)
            {
                // إنشاء الجلسة
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, engineer.EngineerId.ToString()),
            new Claim(ClaimTypes.Email, engineer.Email)
        };

                var identity = new ClaimsIdentity(claims, "Engineer");
                var principal = new ClaimsPrincipal(identity);

                // تسجيل الدخول
                HttpContext.SignInAsync(principal);

                // التوجيه إلى الملف الشخصي
                return RedirectToAction("Profile", "Engineer");
            }

            // البحث في جدول العملاء
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);

            if (customer != null)
            {
                // إنشاء الجلسة
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
            new Claim(ClaimTypes.Email, customer.Email)
        };

                var identity = new ClaimsIdentity(claims, "Customer");
                var principal = new ClaimsPrincipal(identity);

                // تسجيل الدخول
                HttpContext.SignInAsync(principal);

                // التوجيه إلى الملف الشخصي
                return RedirectToAction("Profile", "Customer");
            }

            // إذا لم يتم العثور على المستخدم في الجداول
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

    }
}
