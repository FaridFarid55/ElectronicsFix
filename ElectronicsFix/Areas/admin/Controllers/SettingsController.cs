namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class SettingsController : Controller
    {
        // Filed
        private readonly ISettings oClsSettings;

        // Constructor
        public SettingsController(ISettings oClsSettings)
        {
            this.oClsSettings = oClsSettings;
        }

        // Method
        [Authorize(Roles = "Admin,Owner")]
        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                // If you have any logic here, add it
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                // Redirect or show an error message
                ModelState.AddModelError(string.Empty, "An error occurred while loading the profile.");
                return View();
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Website()
        {
            try
            {
                var item = oClsSettings.GetById(1);
                var viewModel = new LayoutViewModel
                {
                    Copyright = item.Copyright,
                    LogoUrl = item.Logo
                };

                ViewData["LayoutViewModel"] = viewModel;

                return View(item);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while loading the website settings.");
                return View();
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Edit_Website(int Id)
        {
            try
            {
                var item = oClsSettings.GetById(Id);
                return View(item);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while loading the edit website settings.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbSetting item, IFormFile File)
        {
            try
            {
                // Upload image file if provided
                if (File != null)
                {
                    item.Logo = File.FileName;
                    ModelState.Remove("Logo");
                }
                else ModelState.Remove("File");

                if (!ModelState.IsValid) return View("Edit_Website", item);

                // Save in database
                oClsSettings.Save(item);
                return RedirectToAction("Website");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while saving the website settings.");
                return View("Edit_Website", item);
            }
        }
    }
}
