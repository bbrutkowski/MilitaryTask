using CSharpFunctionalExtensions;
using MilitaryWeb.BussinessLogic.Model;

namespace MilitaryWeb.BussinessLogic.Service.Interface
{
    public interface IProductService
    {
        Task<Result<List<Product>>> CreateProductList(ProductCatalog productCatalog);
        Task<Result> SaveFavoriteProducts(List<FavoriteProduct> products, CancellationToken token);
    }
}
