using MilitaryTask2.Model.New;

namespace MilitaryTask2.Model
{
    public class ProductCatalog
    {
        public List<Offer> Offerts { get; set; } = new();
        public List<ProductDetails> ProductDetails { get; set; } = new();
        public List<SimpleProduct> SimpleProductOfferts { get; set; } = new();
        public List<InternationalProductOffer> InternationatProducts { get; set; } = new();
    }
}
