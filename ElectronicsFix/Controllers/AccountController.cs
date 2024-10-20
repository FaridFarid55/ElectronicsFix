using Microsoft.AspNetCore.Identity;

namespace ElectronicsFix.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomers oClsCustomers;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private ApplicationUser user;

        // constrictor
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signManager, ICustomers customers)
        {
            this._userManager = _userManager;
            this._signManager = _signManager;
            oClsCustomers = customers;
        }

        //
        [HttpGet]
        public async Task<IActionResult> LoginOut()
        {
            await _signManager.SignOutAsync();
            return Redirect("~/");
        }

        //
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginModel);
            }

            try
            {
                var myUser = await _userManager.FindByEmailAsync(loginModel.Email);
                // Attempt to sign in the user
                var loginResult = await _signManager.PasswordSignInAsync(myUser.NormalizedUserName, loginModel.Password, isPersistent: true, lockoutOnFailure: true);

                if (loginResult.Succeeded)
                {
                    // Redirect to the return URL or the home page if none is provided
                    string sReturnUrl = ClsUiHelper.Url(loginModel.ReturnUrl);
                    return Redirect(sReturnUrl);
                }
                else
                {
                    // Invalid login attempt
                    ModelState.AddModelError(string.Empty, "Incorrect login attempt");
                    return View("Login", loginModel);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine("An error occurred during login: " + ex.Message);

                // Add an error message to the ModelState to display it in the view
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later");

                // Optionally, you can show a more detailed message for debugging purposes in development environments
                // ModelState.AddModelError(string.Empty, ex.Message); // Only for development

                return View("Login", loginModel);
            }
        }



        //
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            Customer userModel = new Customer
            {
                ReturnUrl = returnUrl
            };
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer userModel)
        {
            if (!ModelState.IsValid)
                return View(userModel);


            var user = new ApplicationUser
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                // PhoneNumber = userModel.Phone,
                UserName = $"{userModel.FirstName}_{userModel.LastName}",

            };

            try
            {
                // Create the user
                var result = await _userManager.CreateAsync(user, userModel.Password);

                // save customer in database
                //    if (result.Succeeded) oClsCustomers.Save(userModel);



                if (result.Succeeded)
                {
                    // Sign in the user
                    await _signManager.PasswordSignInAsync(user, userModel.Password, isPersistent: true, lockoutOnFailure: true);

                    // Add the default role
                    await _userManager.AddToRoleAsync(user, "Customer");

                    // Check and redirect to the ReturnUrl
                    string returnUrl = ClsUiHelper.Url(userModel.ReturnUrl);
                    return Redirect(returnUrl);
                }

                // Display errors in case of failure
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error during registration: {ex.Message}");
                // Display a general error message to the user
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
            }

            return View(userModel);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
