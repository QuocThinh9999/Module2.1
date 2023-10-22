using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Module2._1.Models;

namespace Module2._1.Controllers
{
    public class HomeController : Controller
    {

        private readonly QuanLyDuLieuContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(QuanLyDuLieuContext dbContext, ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }

        [Route("chi-tiet/{id}")]
        public IActionResult Privacy(string id)
        {
            var products = _dbContext.Products.FirstOrDefault(c => c.ProductId == id);
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("danh-sach")]
        public IActionResult Index(string? timkiem)
        {
            var items = _dbContext.Products.Where(c => c.ProductName.Contains((timkiem ?? "").ToLower())).ToList();
            return View(items);
        }
        

    }
}