namespace ElectronicsFix.Controllers
{
    public class ItemController : Controller
    {
        // Filed
        private readonly ILogger<HomeController> _logger;
        private readonly DepiProjectContext _Context;

        // Constrictor

        public ItemController(DepiProjectContext context)
        {
            _Context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel
            {
                Items = _Context.Items,
                ItemDetails = _Context.ItemDetails,
                ItemDiscounts = _Context.ItemDiscounts
            };

            ViewBag.newItems = homeIndexViewModel;

            var item = _Context.Items.FirstOrDefault(i => i.ItemId == id);

            if (item == null)
                return NotFound();

            var itemViewModel = new ItemViewModel
            {
                Item = item,
                ItemDetail = _Context.ItemDetails.FirstOrDefault(i => i.ItemDetailsId == item.ItemDetailsId),
                //ItemDetail = _Context.ItemDetails.FirstOrDefault(itemDetail => _Context.Items.Any(i => i.ItemDetailsId == itemDetail.ItemDetailsId)),
                ItemDiscount = _Context.ItemDiscounts.FirstOrDefault(i => i.ItemId == id),
                Category = _Context.Categories.FirstOrDefault(c => c.CategoryId == item.CategoryId)
            };

            if (itemViewModel.ItemDetail == null || itemViewModel.ItemDiscount == null || itemViewModel.Category == null)
                return NotFound();
            else
                return View(itemViewModel);
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
