using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.BussinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<Product>>> CreateProductList(ProductCatalog productCatalog);
        Task<Result> SaveFavoriteProducts(List<FavoriteProduct> products, CancellationToken token);
    }
}
