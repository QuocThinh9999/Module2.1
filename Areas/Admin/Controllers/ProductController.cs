using Microsoft.AspNetCore.Mvc;
using Module2._1.Common;
using Module2._1.Models;

namespace Module2._1.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Route("quan-ly-sanpham")]
        public class ProductController : Controller
        {
            private readonly QuanLyDuLieuContext _dbContext;

            public ProductController(QuanLyDuLieuContext dbContext)
            {
                _dbContext = dbContext;
            }
            [Route("danh-sach")]
            public IActionResult Index(string? timkiem)
            {
                var items = _dbContext.Products.Where(c => c.ProductName.Contains((timkiem ?? "").ToLower())).ToList();
                return View(items);
            }

            [Route("them-sanpham")]
            public IActionResult Them()
            {
                return View();
            }

            [Route("them-sanpham")]
            [HttpPost]
            public IActionResult Them(string product_name, int price, string gioithieu, IFormFile hinhanh)
            {
                if (!string.IsNullOrEmpty(product_name))
                {
                    Product product = new Product();
                    product.ProductId = Guid.NewGuid().ToString();
                    product.ProductName = product_name;
                    product.Price = price;
                    product.Description = gioithieu;
                    product.UrlImage = UploadFiles.SaveImage(hinhanh);
                    _dbContext.Add(product);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "Product"); // Chuyển hướng đến action "Index" trong controller "Home"
                }
                return View();
            }

            [Route("capnhat-sanpham")]
            public IActionResult CapNhat(string id)
            {
                var item = _dbContext.Products.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return View(item);
            }

            [Route("capnhat-sanpham")]
            [HttpPost]
            public IActionResult CapNhat(string id, string product_name, int price, string gioithieu, IFormFile hinhanh)
            {
                var item = _dbContext.Products.Find(id);
                if (item == null)
                {
                    return NotFound();
                }

                item.ProductName = product_name;

                item.Price = price;

                item.Description = gioithieu;

                if (hinhanh != null)
                {
                    // Xóa ảnh cũ nếu có
                    UploadFiles.RemoveImage(item.UrlImage);

                    // Lưu ảnh mới
                    item.UrlImage = UploadFiles.SaveImage(hinhanh);
                }

                _dbContext.Update(item);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Product"); // Chuyển hướng đến action "Index" trong controller "Home"
            }

            [Route("xoa-sanpham")]
            public IActionResult Xoa(string id)
            {
                var item = _dbContext.Products.Find(id);
                if (item == null)
                {
                    return NotFound();
                }

                // Xóa ảnh liên quan
                UploadFiles.RemoveImage(item.UrlImage);

                _dbContext.Remove(item);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Product"); // Chuyển hướng đến action "Index" trong controller "Home"
            }
        [Route("chi-tiet/{id}")]
        public IActionResult chitiet(string id)
            {
            var products = _dbContext.Products.FirstOrDefault(c => c.ProductId == id);
            return View(products); 
            }
        }
    
}
