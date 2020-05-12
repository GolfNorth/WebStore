using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admins")]
    public class ProductsController : Controller
    {
        private readonly WebStoreDB _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private const string ImagePath = "/images/shop/";

        public ProductsController(WebStoreDB db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var webStoreContext = _db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(await webStoreContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_db.Brands, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Id");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,BrandId,ImageUrl,Price,Order,Id,Name")] Product product, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                _db.Add(product);
                await _db.SaveChangesAsync();
                if (imageFile != null) // Обработка загруженного файла
                {
                    product.ImageUrl = $"product{product.Id}{Path.GetExtension(imageFile.FileName)}";
                    await using var fileStream = new FileStream(_appEnvironment.WebRootPath + ImagePath + product.ImageUrl, FileMode.Create);
                    await imageFile.CopyToAsync(fileStream);
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_db.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Id", product.CategoryId);

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_db.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,BrandId,ImageUrl,Price,Order,Id,Name")] Product product, IFormFile imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(product);
                    await _db.SaveChangesAsync();
                    if (imageFile != null) // Обработка загруженного файла
                    {
                        product.ImageUrl = $"product{product.Id}{Path.GetExtension(imageFile.FileName)}";
                        await using var fileStream = new FileStream(_appEnvironment.WebRootPath + ImagePath + product.ImageUrl, FileMode.Create);
                        await imageFile.CopyToAsync(fileStream);
                        await _db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_db.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);
            var imageFile = new FileInfo(_appEnvironment.WebRootPath + ImagePath + product.ImageUrl);
            if (imageFile.Exists) imageFile.Delete();
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Any(e => e.Id == id);
        }
    }
}
