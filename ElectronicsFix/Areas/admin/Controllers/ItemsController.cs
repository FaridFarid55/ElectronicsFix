namespace ElectronicsFix.Areas.admin.Controllers
{
    // Specifies that this controller belongs to the "admin" area
    [Area("admin")]
    public class ItemsController : Controller
    {
        // Field for database context
        private readonly DepiProjectContext _context;

        // Constructor initializes the database context
        public ItemsController(DepiProjectContext context)
        {
            _context = context;
        }

        //This  Method Items
        #region Method


        // GET: Items - Displays the list of items, optionally filtered by category
        public async Task<IActionResult> Index(int? categoryId)
        {
            try
            {
                // Fetching the list of categories from the database where OnDelete is false
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();

                // Fetching items either by categoryId or all items if no categoryId is provided
                var items = categoryId == null
                    ? await _context.Items.Include(i => i.ItemDetails).Include(i => i.Category).ToListAsync()
                    : await _context.Items
                        .Include(i => i.ItemDetails)
                        .Include(i => i.Category)
                        .Where(i => i.CategoryId == categoryId && i.OnDelete == false)
                        .ToListAsync();

                return View(items);
            }
            catch (Exception ex)
            {
                // Handle error and return empty view if something goes wrong
                ModelState.AddModelError("", "An error occurred while loading the items.");
                return View(new List<Item>());
            }
        }

        // GET: Items/Details/5 - Displays details of a specific item by id
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                // Fetching the item by id and including related ItemDetails and Category
                var item = await _context.Items
                    .Include(i => i.ItemDetails)
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong while retrieving details
                ModelState.AddModelError("", "An error occurred while retrieving the item details.");
                return View();
            }
        }

        // GET: Items/Create - Displays the form to create a new item
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // Fetching categories to populate the dropdown list for category selection
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
                return View();
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong while preparing the create form
                ModelState.AddModelError("", "An error occurred while preparing the create form.");
                return View();
            }
        }

        // POST: Items/Create - Processes the creation of a new item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,SalesPrice,PurchasePrice,CategoryId,ItemType,ImagePath,Description,ItemDetailsId")] Item item, IFormFile File)
        {
            try
            {
                // Upload image file if provided and assign to ImagePath property
                if (File != null)
                {
                    item.ImagePath = await ClsUiHelper.UploadImage(File, "Items");

                    ModelState.Remove("ImagePath");
                }



                // Return the form if the model state is invalid
                if (!ModelState.IsValid) return View(item);

                // Set the created date for item details and add item to the database
                item.ItemDetails.CreatedDate = DateTime.Now;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong during the creation process
                ModelState.AddModelError("", "An error occurred while creating the item.");
                return View(item);
            }
        }

        // GET: Items/Edit/5 - Displays the form to edit an existing item
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                // Fetching the item to be edited by id
                var item = await _context.Items.FindAsync(id);
                if (item == null) return NotFound();

                // Fetching categories to populate the dropdown list for category selection
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(item);
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong while preparing the edit form
                ModelState.AddModelError("", "An error occurred while preparing the edit form.");
                return View();
            }
        }

        // POST: Items/Edit/5 - Processes the updates to an existing item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,SalesPrice,PurchasePrice,CategoryId,ItemType,ImagePath,Description,ItemDetailsId")] Item item)
        {
            if (id != item.ItemId) return NotFound();

            if (!ModelState.IsValid)
            {
                // If model state is invalid, reload categories and return the view
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(item);
            }

            try
            {
                // Update the item details and save changes to the database
                item.ItemDetails.UpdatedDate = DateTime.Now;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemId)) return NotFound();
                else throw;
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong during the update process
                ModelState.AddModelError("", "An error occurred while editing the item.");
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(item);
            }
        }

        // GET: Items/Delete/5 - Displays the confirmation view for deleting an item
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                // Fetching the item to be deleted by id
                var item = await _context.Items
                    .Include(i => i.ItemDetails)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong while preparing the delete form
                ModelState.AddModelError("", "An error occurred while preparing the delete form.");
                return View();
            }
        }

        // POST: Items/Delete/5 - Processes the deletion of an item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Remove related records in Orders and ItemDiscounts tables
                var orders = await _context.Orders.Where(o => o.ItemId == id).ToListAsync();
                _context.Orders.RemoveRange(orders);

                var itemDiscounts = await _context.ItemDiscounts.Where(d => d.ItemId == id).ToListAsync();
                _context.ItemDiscounts.RemoveRange(itemDiscounts);

                // Mark the item as deleted by setting OnDelete to true and update the item
                var item = await _context.Items.FindAsync(id);
                if (item == null) return NotFound();

                item.OnDelete = true;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Handle error if something goes wrong during the delete process
                ModelState.AddModelError("", "An error occurred while deleting the item.");
                return View();
            }
        }

        // Check if an item exists by id
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
        #endregion
    }
}
