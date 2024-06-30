using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.BussinessLogic.Services.Interfaces;
using MilitaryASPWeb.Repository.Interface;

namespace MilitaryASPWeb.BussinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<Result> SaveFavoriteProductsAsync(List<FavoriteProduct> products, CancellationToken token)
        {
            try
            {
                var savingResult = await _productRepository.SaveProductsAsync(products, token);
                return Result.Success(savingResult);
            }
            catch (ApplicationException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
