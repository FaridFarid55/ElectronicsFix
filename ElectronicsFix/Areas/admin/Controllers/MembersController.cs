using Microsoft.AspNetCore.Identity;

namespace ElectronicsFix.Areas.admin.Controllers
{
    [Authorize(Roles = "Owner")]
    [Area("admin")]
    public class MembersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MembersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userRolesViewModel = new List<UserRolesViewModel>();

                foreach (var user in users)
                {
                    if (user.Id == "c1d6bcfa-54b0-44aa-b68e-6aaed089a094") continue;

                    var thisViewModel = new UserRolesViewModel
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Roles = await _userManager.GetRolesAsync(user)
                    };

                    userRolesViewModel.Add(thisViewModel);
                }

                return View(userRolesViewModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading users.");
                return View(new List<UserRolesViewModel>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = await _roleManager.Roles.ToListAsync();

                var model = new EditUserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    AvailableRoles = roles.Select(r => r.Name).ToList(),
                    SelectedRoles = new List<string>() // Ensure the selected roles are empty
                };

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading user roles.");
                return View(new EditUserRolesViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserRolesViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                var selectedRoles = model.SelectedRoles ?? new List<string>(); // Ensure it's not null

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to remove existing roles.");
                    return View(model);
                }

                var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to add new roles.");
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while updating user roles.");
                return View(model);
            }
        }
    }
}
