using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.Models.Model;

namespace MilitaryASPWeb.BussinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<Product>>> CreateProductList(ProductCatalog productCatalog);
        Task<Result> SaveFavoriteProductsAsync(List<FavoriteProduct> products, CancellationToken token);
    }
}
