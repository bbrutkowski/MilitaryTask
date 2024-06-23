using Microsoft.AspNetCore.Mvc;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.BussinessLogic.Services.Interfaces;

namespace MilitaryASPWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IProductService _productService;

        public ProductController(IFileService fileService, IProductService productService)
        {
            _fileService = fileService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var productCatalog = await _fileService.ProcessXmlFiles();
            var products = await _productService.CreateProductList(productCatalog.Value);

            return View(products.Value);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavorites([FromBody] List<FavoriteProduct> favoriteItems, CancellationToken token)
        {
            if (!favoriteItems.Any()) return BadRequest();

            var result = await _productService.SaveFavoriteProducts(favoriteItems, token);
            if (result.IsSuccess) return Ok();

            return BadRequest();
        }
    }
}
