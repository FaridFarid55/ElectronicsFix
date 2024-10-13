using Microsoft.AspNetCore.Mvc;
using Domains; // تأكد من أن لديك المساحات الصحيحة
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
public class ItemsController : Controller
{
    private readonly DepiProjectContext _context;

    public ItemsController(DepiProjectContext context)
    {
        _context = context;
    }

    // GET: Items
    public async Task<IActionResult> Index(int? categoryId)
    {
        // جلب قائمة الفئات من قاعدة البيانات
        ViewBag.Categories = await _context.Categories.ToListAsync();

        // إذا تم تحديد فئة، جلب العناصر المرتبطة بتلك الفئة، وإلا جلب جميع العناصر
        var items = categoryId == null
            ? await _context.Items.Include(i => i.ItemDetails).Include(i => i.Category).ToListAsync()
            : await _context.Items
                .Include(i => i.ItemDetails)
                .Include(i => i.Category)
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();

        return View(items);
    }

    // GET: Items/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // إضافة تضمين الفئة لتجنب NullReferenceException
        var item = await _context.Items
            .Include(i => i.ItemDetails)
            .Include(i => i.Category)  // تضمين الفئة
            .FirstOrDefaultAsync(m => m.ItemId == id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    // GET: Items/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // جلب قائمة الفئات من قاعدة البيانات
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View();
    }

    // POST: Items/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ItemId,ItemName,SalesPrice,PurchasePrice,CategoryId,ItemType,ImagePath,Description,ItemDetailsId")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Items/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        // جلب قائمة الفئات من قاعدة البيانات
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(item);
    }

    // POST: Items/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,SalesPrice,PurchasePrice,CategoryId,ItemType,ImagePath,Description,ItemDetailsId")] Item item)
    {
        if (id != item.ItemId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
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
        // إذا كان هناك خطأ في النموذج، أعد تحميل قائمة الفئات مرة أخرى
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(item);
    }

    // GET: Items/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var item = await _context.Items
            .Include(i => i.ItemDetails)
            .FirstOrDefaultAsync(m => m.ItemId == id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    // POST: Items/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // حذف السجلات المرتبطة في جدول Orders
        var orders = await _context.Orders.Where(o => o.ItemId == id).ToListAsync();
        _context.Orders.RemoveRange(orders);

        // حذف السجلات المرتبطة في جدول ItemDiscounts (إذا كان ذلك مناسبًا)
        var itemDiscounts = await _context.ItemDiscounts.Where(d => d.ItemId == id).ToListAsync();
        _context.ItemDiscounts.RemoveRange(itemDiscounts);

        // الآن حذف العنصر من جدول Items
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemExists(int id)
    {
        return _context.Items.Any(e => e.ItemId == id);
    }
}
