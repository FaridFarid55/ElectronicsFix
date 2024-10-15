namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class ItemsController : Controller
    {
        private readonly DepiProjectContext _context;

        public ItemsController(DepiProjectContext context)
        {
            _context = context;
        }

        #region Methods

        // GET: Items
        public async Task<IActionResult> Index(int? categoryId)
        {
            try
            {
                // Load categories that are not marked for deletion
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();

                // Load items based on category filter
                var items = categoryId == null
                    ? await _context.Items.Include(i => i.ItemDetails).Include(i => i.Category).Where(i => i.OnDelete == false).ToListAsync()
                    : await _context.Items
                        .Include(i => i.ItemDetails)
                        .Include(i => i.Category)
                        .Where(i => i.CategoryId == categoryId && i.OnDelete == false)
                        .ToListAsync();

                return View(items);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while loading the items: {ex.Message}");
                return View(new List<Item>());
            }
        }

        // GET: Items/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var item = await _context.Items
                    .Include(i => i.ItemDetails)
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving the item details: {ex.Message}");
                return View();
            }
        }

        // GET: Items/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Load categories for the dropdown
            ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item, IFormFile? File)
        {
            // Upload image file if provided
            if (File != null)
            {
                item.ImagePath = await ClsUiHelper.UploadImage(File, "Items");
                ModelState.Remove("ImagePath");
            }

            // Validate the data
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
                return View(item);
            }

            try
            {
                // Check if item details are provided
                if (item.ItemDetails != null)
                {
                    item.ItemDetails.CreatedDate = DateTime.Now;
                    _context.Add(item.ItemDetails);
                    await _context.SaveChangesAsync();
                }

                _context.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the item: {ex.Message}");
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
                return View(item);
            }
        }

        // GET: Items/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.ItemDetails)
                .FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null) return NotFound();

            ViewBag.Categories = await _context.Categories.ToListAsync(); // Load categories for the dropdown
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item, IFormFile? File)
        {
            // Upload image file if provided
            if (File != null)
            {
                item.ImagePath = await ClsUiHelper.UploadImage(File, "Items");
                ModelState.Remove("ImagePath");
            }

            if (id != item.ItemId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (item.ItemDetails != null)
                    {
                        item.ItemDetails.UpdatedDate = DateTime.Now;
                        _context.Add(item.ItemDetails);
                        await _context.SaveChangesAsync();
                    }

                    // Update the item
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _context.Categories.ToListAsync(); // Load categories if there's an error
            return View(item);
        }

        // GET: Items/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var item = await _context.Items
                    .Include(i => i.ItemDetails)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (item == null) return NotFound();

                return View(item);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while preparing the delete form: {ex.Message}");
                return View();
            }
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.ItemId == id).ToListAsync();
                _context.Orders.RemoveRange(orders);

                var itemDiscounts = await _context.ItemDiscounts.Where(d => d.ItemId == id).ToListAsync();
                _context.ItemDiscounts.RemoveRange(itemDiscounts);

                var item = await _context.Items.FindAsync(id);
                if (item == null) return NotFound();

                item.OnDelete = true;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while deleting the item: {ex.Message}");
                return View();
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        #endregion
    }
}
