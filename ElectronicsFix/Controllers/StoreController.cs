using Domains;
using ElectronicsFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsFix.Controllers
{
	public class StoreController : Controller
	{
		// Filed
		private readonly ILogger<HomeController> _logger;
		private readonly DepiProjectContext _context;

		// Constrictor
		//public HomeController(ILogger<HomeController> logger, DepiProjectContext context)
		//{
		//    _logger = logger;
		//    _context = context;
		//}
		//public HomeController(ILogger<HomeController> logger)
		//{
		//    _logger = logger;

		//}
		public StoreController(DepiProjectContext context)
		{
			_context = context;
		}

		public IActionResult Index
			(int pageNumber = 1, string category = "All Categories", string brand = "All Brands", string sort = "default")
		{
			var types = _context.Items.Select(item => item.ItemType).Distinct().ToList();
			ViewBag.Types = types;        // represent ItemType

			var brands = _context.Categories.Select(cat => cat.CategoryName).Distinct().ToList();
			ViewBag.Brands = brands;      // represent Category
			
			IEnumerable<Item> Items ;

			// Filtering
			if (category == "All Categories" && brand == "All Brands")
			{
				Items = _context.Items.ToList();
			}
			else if (category != "All Categories" && brand == "All Brands")
			{
				Items = _context.Items.Where(item => item.ItemType == category);
			}
			else if (category == "All Categories" && brand != "All Brands")
			{
				var key = _context.Categories.Where(cat => cat.CategoryName == brand)
					.Select(cat => cat.CategoryId).FirstOrDefault();
				Items = _context.Items.Where(item => item.CategoryId == key).ToList();
			}
			else
			{
				Items = _context.Items.ToList();
			}

			// Calculate number of pages
			int pageSize = 9;
			int totalItems = Items.Count();
			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			// Sort
			switch (sort)
			{
				case "name":
					Items = Items.OrderBy(item => item.ItemName)
						.Skip((pageNumber - 1) * pageSize).Take(pageSize);
					break;
				case "price":
					Items = Items.OrderByDescending(item => item.SalesPrice)
						.Skip((pageNumber - 1) * pageSize).Take(pageSize);
					break;
				case "latest":
					Items = (from item in Items
							 join detail in _context.ItemDetails
							 on item.ItemDetailsId equals detail.ItemDetailsId
							 orderby detail.CreatedDate descending
							 select item)
							 .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
					break;
				default:
					break;
			}

			//StoreItemsViewModel storeItemsviewModel = new StoreItemsViewModel
			//{
			//	Items = Items.Skip((pageNumber - 1) * pageSize).Take(pageSize),
			//	ItemDetails = _context.ItemDetails,
			//	ItemDiscounts = _context.ItemDiscounts,
			//	Categories = _context.Categories
			//};

			// ViewModel
			PageItemsViewModel viewModel = new PageItemsViewModel
			{
				StoreItemsViewModel = new StoreItemsViewModel
				{
					Items = Items,
					ItemDetails = _context.ItemDetails,
					ItemDiscounts = _context.ItemDiscounts,
					Categories = _context.Categories
				},
				PageNumber = pageNumber,
				TotalPages = totalPages,
				Category = category,
				Brand = brand,
				Sort = sort
			};

			return View(viewModel);
		}
	}
}
