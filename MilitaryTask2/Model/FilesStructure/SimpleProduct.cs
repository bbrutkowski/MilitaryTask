namespace MilitaryTask2.Model.New
{
    public class SimpleProduct
    {
        public string EAN { get; set; }
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public List<Category> Categories { get; set; }
        public string Unit { get; set; }
        public string Weight { get; set; }
        public string PKWiU { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public string PriceAfterDiscountNet { get; set; }
        public decimal RetailPriceGross { get; set; }
        public List<Photo> Photos { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public int Main { get; set; }
        public string Url { get; set; }
    }
}
