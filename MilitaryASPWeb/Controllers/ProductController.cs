using Microsoft.AspNetCore.Mvc;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.BussinessLogic.Services.Interfaces;
using MilitaryASPWeb.Models.Services.Interfaces;

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
            var productCatalogResult = await _fileService.ProcessXmlFiles();
            if (productCatalogResult.IsFailure) return BadRequest();

            var createListResult = await _productService.CreateProductList(productCatalogResult.Value);
            if (createListResult.IsFailure) return BadRequest();        

            return View(createListResult.Value);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavoritesAsync([FromBody] List<FavoriteProduct> favoriteItems, CancellationToken token)
        {
            if (!favoriteItems.Any()) return BadRequest();

            var savingResult = await _productService.SaveFavoriteProductsAsync(favoriteItems, token);
            if (savingResult.IsFailure) return BadRequest();

            return Ok();
        }
    }
}
