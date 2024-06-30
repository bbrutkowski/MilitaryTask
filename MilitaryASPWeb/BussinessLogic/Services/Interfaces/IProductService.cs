using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.BussinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result> SaveFavoriteProductsAsync(List<FavoriteProduct> products, CancellationToken token);
    }
}
