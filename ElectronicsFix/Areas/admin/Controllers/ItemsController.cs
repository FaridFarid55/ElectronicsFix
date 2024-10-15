using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        #region Method

        public async Task<IActionResult> Index(int? categoryId)
        {
            try
            {
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();

                var items = categoryId == null
                    ? await _context.Items.Include(i => i.ItemDetails).Include(i => i.Category).Where(i => i.OnDelete == false).ToListAsync()
                    : await _context.Items
                        .Include(i => i.ItemDetails)
                        .Include(i => i.Category)
                        .Where(i => i.CategoryId == categoryId && i.OnDelete == false)
                        .ToListAsync();

                return View(items);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while loading the items.");
                return View(new List<Item>());
            }
        }

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
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while retrieving the item details.");
                return View();
            }
        }

        // GET: Items/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
            return View();
        }

        // POST: Items/Create
        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item, ItemDetail itemDetail, IFormFile ImagePath)
        {
            // تحقق من تحميل الصورة
            if (ImagePath != null && ImagePath.Length > 0)
            {
                // Upload image
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
                var fileStream = new FileStream(Path.Combine("wwwroot", "Uploads", "Items", imageName), FileMode.Create);
                await ImagePath.CopyToAsync(fileStream);
                item.ImagePath = imageName;
            }

            // تحقق من صحة البيانات
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
                return View(item);
            }

            try
            {
                // الربط بين الـ Item والـ ItemDetail
                item.ItemDetails = itemDetail;
                item.ItemDetails.CreatedDate = DateTime.Now;

                // حفظ البيانات
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while creating the item.");
                ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
                return View(item);
            }
        }

        // GET: Items/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // جلب العنصر مع التفاصيل الخاصة به من قاعدة البيانات
            var item = _context.Items
                .Include(i => i.ItemDetails) // تضمين التفاصيل
                .FirstOrDefault(i => i.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            // تمرير البيانات إلى الـ View لعرض النموذج
            ViewBag.Categories = _context.Categories.ToList(); // لملء قائمة الفئات
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // تحديث العنصر مع التفاصيل الخاصة به
                    _context.Update(item);
                    _context.SaveChanges();
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

            ViewBag.Categories = _context.Categories.ToList(); // لملء قائمة الفئات إذا حدث خطأ
            return View(item);
        }

        // للتحقق إذا كان العنصر موجودًا أم لا
        //private bool ItemExists(int id)
        //{
        //    return _context.Items.Any(e => e.ItemId == id);
        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null) return NotFound();

        //    try
        //    {
        //        var item = await _context.Items.FindAsync(id);
        //        if (item == null) return NotFound();

        //        ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
        //        return View(item);
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "An error occurred while preparing the edit form.");
        //        return View();
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,SalesPrice,PurchasePrice,CategoryId,ItemType,ImagePath,Description")] Item item)
        //{
        //    if (id != item.ItemId) return NotFound();

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
        //        return View(item);
        //    }

        //    try
        //    {
        //        item.ItemDetails.UpdatedDate = DateTime.Now;
        //        _context.Update(item);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ItemExists(item.ItemId)) return NotFound();
        //        else throw;
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "An error occurred while editing the item.");
        //        ViewBag.Categories = await _context.Categories.Where(a => a.OnDelete == false).ToListAsync();
        //        return View(item);
        //    }
        //}

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
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while preparing the delete form.");
                return View();
            }
        }

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
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while deleting the item.");
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
