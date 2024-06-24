using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.DataContext;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.Repository.Interface;

namespace MilitaryASPWeb.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext dataContext) => _context = dataContext;

        public async Task<Result> SaveProductsAsync(List<FavoriteProduct> products, CancellationToken token)
        {
            try
            {
                await _context.FavoriteProducts.AddRangeAsync(products, token);
                await _context.SaveChangesAsync(token);
                return Result.Success();
            }
            catch (Exception)
            {
                throw new ApplicationException($"An error occurred while saving items" +
                    $"Method: {nameof(SaveProductsAsync)}");
            }
        }
    }
}
