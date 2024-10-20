namespace ElectronicsFix.Areas.Controllers
{

    [Area("admin")]
    [Authorize(Roles = "Admin,Owner")]
    public class HomeController : Controller
    {
        // Filed
        private readonly ILogger<HomeController> _logger;

        // Constrictor
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Method
        public IActionResult Index()
        {
            return View();
        }
    }
}
