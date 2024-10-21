namespace ElectronicsFix.Controllers
{
    public class HomeController : Controller
    {
        // Filed
        private readonly ILogger<HomeController> _logger;
        private readonly DepiProjectContext _Context;

        // Constrictor
        //public HomeController(ILogger<HomeController> logger, DepiProjectContext context)
        //{
        //    _logger = logger;
        //    _Context = context;
        //}
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;

        //}
        public HomeController(DepiProjectContext context)
        {
            _Context = context;
        }

        // Method
        [HttpGet]
        public IActionResult Index()
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {

                Items = _Context.Items.ToList(),
                ItemDetails = _Context.ItemDetails.ToList(),
                ItemDiscounts = _Context.ItemDiscounts.ToList()
            };


            DateTime dateTime = _Context.ItemDiscounts.OrderBy(x => x.EndDate)
                .Select(x => x.EndDate).LastOrDefault();

            //DateTime targetDate = new DateTime(2024, 12, 31, 23, 59, 59);
            DateTime targetDate = dateTime;
            DateTime currentDate = DateTime.Now;

            TimeSpan timeRemaining = targetDate - currentDate;

            ViewBag.Days = timeRemaining.Days;
            ViewBag.Hours = timeRemaining.Hours;
            ViewBag.Minutes = timeRemaining.Minutes;
            ViewBag.Seconds = timeRemaining.Seconds;


            var topSelling = _Context.Orders.GroupBy(order => order.ItemId)
                .Select(x => new TopSelling
                {
                    ItemId = x.Key, // represents the value you grouped by which is ItemID
                    TotalSold = x.Sum(order => order.Quantity)
                }).OrderByDescending(x => x.TotalSold).ToList();


            ViewBag.TopSelling = topSelling;

            return View(viewModel);
        }
    }
}
