using CSharpFunctionalExtensions;
using MilitaryWeb.BussinessLogic.DataContext;
using MilitaryWeb.BussinessLogic.Model;
using MilitaryWeb.Repository.Interface;

namespace MilitaryWeb.Repository
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
			catch (Exception e)
			{
                await Console.Out.WriteLineAsync($"An error occurred while saving items: {e.Message}" +
					$"Method: {nameof(SaveProductsAsync)}");
				return Result.Failure($"{e.Message}");
			}
        }
    }
}
