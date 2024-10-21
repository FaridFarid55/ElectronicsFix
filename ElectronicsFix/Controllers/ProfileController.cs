//using System.Security.Claims;

//namespace ElectronicsFix.Controllers
//{

//    public class ProfileController : Controller
//    {
//        private readonly DepiProjectContext _context;

//        public ProfileController(DepiProjectContext context)
//        {
//            _context = context;
//        }

//        // GET: Profile
//        [HttpGet]
//        public IActionResult Profile()
//        {
//            // الحصول على البريد الإلكتروني الخاص بالمستخدم من الجلسة
//            var email = User.FindFirst(ClaimTypes.Email)?.Value;
//            var userRole = GetUserRole(); // الحصول على الدور الخاص بالمستخدم

//            var user = GetUserByEmailAndRole(email, userRole); // البحث عن المستخدم بناءً على البريد الإلكتروني والدور
//            if (user != null)
//            {
//                return View("Profile", user);
//            }

//            // إذا لم يتم العثور على المستخدم
//            return NotFound();
//        }

//        // GET: Profile/Edit
//        [HttpGet]
//        public IActionResult Edit()
//        {
//            var email = User.FindFirst(ClaimTypes.Email)?.Value;
//            var userRole = GetUserRole(); // الحصول على الدور الخاص بالمستخدم

//            var user = GetUserByEmailAndRole(email, userRole); // البحث عن المستخدم بناءً على البريد الإلكتروني والدور
//            if (user != null)
//            {
//                return View("Edit", user);
//            }

//            return NotFound();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit([Bind("FirstName,LastName,Phone,Address,Password,ConfirmPassword")] User user)
//        {
//            var email = User.FindFirst(ClaimTypes.Email)?.Value;
//            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
//            var userRole = GetUserRole();


//            var Mebers = GetUserByEmailAndRole(email, userRole);
//            if (existingUser == null)
//            {
//                return NotFound();
//            }

//            ModelState.Remove("Email");

//            if (!ModelState.IsValid)
//            {
//                return View(user);
//            }
//            try
//            {

//                _context.Entry(Mebers).State = EntityState.Modified;
//                _context.SaveChanges();

//                existingUser.FirstName = user.FirstName;
//                existingUser.LastName = user.LastName;
//                existingUser.PhoneNumber = user.Phone;

//                _context.Entry(existingUser).State = EntityState.Modified;
//                _context.SaveChanges();


//                TempData["SuccessMessage"] = "User details updated successfully.";
//                return RedirectToAction("Profile");
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
//                return View(user);
//            }
//        }

//        // الحصول على الدور الخاص بالمستخدم الحالي
//        private string GetUserRole()
//        {
//            if (User.IsInRole("Customer")) return "Customer";
//            if (User.IsInRole("Admin")) return "Admin";
//            if (User.IsInRole("Engineer")) return "Engineer";
//            return null;
//        }

//        // البحث عن المستخدم بناءً على البريد الإلكتروني والدور
//        private object GetUserByEmailAndRole(string email, string role)
//        {
//            if (role == "Customer")
//            {
//                return _context.Customers.FirstOrDefault(c => c.Email == email);
//            }
//            else if (role == "Admin")
//            {
//                return _context.Admins.FirstOrDefault(a => a.Email == email);
//            }
//            else if (role == "Engineer")
//            {
//                return _context.Engineers.FirstOrDefault(e => e.Email == email);
//            }
//            return null;
//        }
//    }
//}
