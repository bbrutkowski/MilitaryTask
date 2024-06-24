using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.Models.Model
{
    public class ProductCatalog
    {
        public List<Offer> Offerts { get; set; } = new();
        public List<ProductDetails> ProductDetails { get; set; } = new();
        public List<SimpleProduct> SimpleProductOfferts { get; set; } = new();
        public List<InternationalProduct> InternationatProducts { get; set; } = new();
    }
}
