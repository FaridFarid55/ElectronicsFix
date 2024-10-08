namespace ElectronicsFix.Areas.Controllers
{

    [Area("admin")]
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
