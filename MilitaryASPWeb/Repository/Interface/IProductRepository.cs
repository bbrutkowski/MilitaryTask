using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Result> SaveProductsAsync(List<FavoriteProduct> products, CancellationToken token);
    }
}
