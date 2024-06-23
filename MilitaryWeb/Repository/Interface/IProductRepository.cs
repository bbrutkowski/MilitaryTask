using CSharpFunctionalExtensions;
using MilitaryWeb.BussinessLogic.Model;

namespace MilitaryWeb.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Result> SaveProductsAsync(List<FavoriteProduct> products, CancellationToken token);
    }
}
