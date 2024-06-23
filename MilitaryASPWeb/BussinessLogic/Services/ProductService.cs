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

        public async Task<Result<List<Product>>> CreateProductList(ProductCatalog productCatalog)
        {
            if (productCatalog is null) return Result.Failure<List<Product>>("No product catalog");

            var products = new List<Product>();

            foreach (var offer in productCatalog.Offerts)
            {
                products.Add(MapOfferToProduct(offer));
            }

            foreach (var productDetail in productCatalog.ProductDetails)
            {
                products.Add(MapProductDetailsToProduct(productDetail));
            }

            foreach (var simpleProductOffer in productCatalog.SimpleProductOfferts)
            {
                products.Add(MapSimpleProductOfferToProduct(simpleProductOffer));
            }

            foreach (var internationalProduct in productCatalog.InternationatProducts)
            {
                products.Add(MapInternationalProductToProduct(internationalProduct));
            }

            return Result.Success(products);
        }

        private Product MapInternationalProductToProduct(InternationalProduct internationalProduct)
        {
            try
            {
                return new Product()
                {
                    Id = internationalProduct.ID,
                    Description = internationalProduct.Description,
                    Quantity = default,
                    Photo = internationalProduct.Photo
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during mapping: {e.Message}" +
                    $"Method: {nameof(MapInternationalProductToProduct)}");
                return new();
            }
        }

        private Product MapSimpleProductOfferToProduct(SimpleProduct simpleProductOffer)
        {
            try
            {
                return new Product()
                {
                    Id = simpleProductOffer.ID,
                    Description = simpleProductOffer.Description,
                    Quantity = simpleProductOffer.Quantity,
                    Photo = simpleProductOffer.Photo
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during mapping: {e.Message}" +
                    $"Method: {nameof(MapSimpleProductOfferToProduct)}");
                return new();
            }
        }

        private Product MapProductDetailsToProduct(ProductDetails productDetail)
        {
            try
            {
                return new Product()
                {
                    Id = productDetail.ID,
                    Description = string.Empty,
                    Quantity = productDetail.Quantity,
                    Photo = string.Empty
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during mapping: {e.Message}" +
                    $"Method: {nameof(MapProductDetailsToProduct)}");
                return new();
            }
        }

        private Product MapOfferToProduct(Offer offer)
        {
            try
            {
                return new Product()
                {
                    Id = offer.Id,
                    Description = string.Empty,
                    Quantity = offer.StockQuantity,
                    Photo = string.Empty
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during mapping: {e.Message}" +
                    $"Method: {nameof(MapOfferToProduct)}");
                return new();
            }
        }

        public async Task<Result> SaveFavoriteProducts(List<FavoriteProduct> products, CancellationToken token)
        {
            return Result.Success(await _productRepository.SaveProductsAsync(products, token));
        }
    }
}
