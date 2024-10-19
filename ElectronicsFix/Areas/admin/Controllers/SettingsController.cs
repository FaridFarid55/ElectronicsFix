namespace ElectronicsFix.Areas.admin.Controllers
{

    [Area("admin")]
    public class SettingsController : Controller
    {
        // Filed
        private readonly ISettings oClsSettings;

        //Constrictor
        public SettingsController(ISettings oClsSettings)
        {
            this.oClsSettings = oClsSettings;
        }
        // Method
        //[Authorize(Roles = "Admin,Data Entry,Owner")]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        //[Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Website()
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

        //  [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult Edit_Website(int Id)
        {
            return View(oClsSettings.GetById(Id));
        }

        [HttpPost]
        public IActionResult Save(TbSetting item, IFormFile File)
        {
            // Upload image file if provided
            if (File != null)
            {
                item.Logo = File.FileName;
                ModelState.Remove("Logo");
            }
            else ModelState.Remove("File");


            if (!ModelState.IsValid) return View("Edit_Website", item);


            // save in database
            oClsSettings.Save(item);

            return RedirectToAction("Website");
        }
    }
}
