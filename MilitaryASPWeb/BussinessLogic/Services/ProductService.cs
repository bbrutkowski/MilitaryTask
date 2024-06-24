using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.BussinessLogic.Services.Interfaces;
using MilitaryASPWeb.Models.Model;
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

            try
            {
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
            catch (ProductMappingException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return Result.Failure<List<Product>>(ex.Message);
            }
        }

        private Product MapInternationalProductToProduct(InternationalProduct internationalProduct)
        {
            try
            {
                return new Product()
                {
                    Id = internationalProduct.Id,
                    Description = internationalProduct.Description,
                    Quantity = default,
                    Photo = internationalProduct.Photo
                };
            }
            catch (Exception)
            {
                throw new ProductMappingException("An error occurred during mapping." +
                    $"Method: {nameof(MapInternationalProductToProduct)}");              
            }
        }

        private Product MapSimpleProductOfferToProduct(SimpleProduct simpleProductOffer)
        {
            try
            {
                return new Product()
                {
                    Id = simpleProductOffer.Id,
                    Description = simpleProductOffer.Description,
                    Quantity = simpleProductOffer.Quantity,
                    Photo = simpleProductOffer.Photo
                };
            }
            catch (Exception)
            {
                throw new ProductMappingException("An error occurred during mapping." +
                    $"Method: {nameof(MapSimpleProductOfferToProduct)}");
            }
        }

        private Product MapProductDetailsToProduct(ProductDetails productDetail)
        {
            try
            {
                return new Product()
                {
                    Id = productDetail.Id,
                    Description = string.Empty,
                    Quantity = productDetail.Quantity,
                    Photo = string.Empty
                };
            }
            catch (Exception)
            {
                throw new ProductMappingException("An error occurred during mapping." +
                   $"Method: {nameof(MapProductDetailsToProduct)}");
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
            catch (Exception)
            {
                throw new ProductMappingException("An error occurred during mapping." +
                    $"Method: {nameof(MapOfferToProduct)}");
            }
        }

        public async Task<Result> SaveFavoriteProductsAsync(List<FavoriteProduct> products, CancellationToken token)
        {
            var savingResult = await _productRepository.SaveProductsAsync(products, token);
            if (savingResult.IsFailure) return Result.Failure("An error occurred while saving products");

            return Result.Success();
        }
    }
}
