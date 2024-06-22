namespace MilitaryTask2.Model.New
{
    public class InternationalProductOffer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        public string DescriptionEn { get; set; }
        public string Code { get; set; }
        public string EAN { get; set; }
        public int Status { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal SuggestedRetailPrice { get; set; }
        public string SupplierCode { get; set; }
        public decimal VAT { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public string CategoryPl { get; set; }
        public string CategoryEn { get; set; }
        public List<string> Photos { get; set; }
    }
}
