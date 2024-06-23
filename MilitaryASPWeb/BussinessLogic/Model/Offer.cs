namespace MilitaryASPWeb.BussinessLogic.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public decimal PriceGross { get; set; }
        public decimal PriceNet { get; set; }
        public decimal Vat { get; set; }
        public decimal SRPGross { get; set; }
        public decimal SRPNet { get; set; }
        public decimal SRPVat { get; set; }
        public int SizeId { get; set; }
        public string SizeCodeProducer { get; set; }
        public string SizeCode { get; set; }
        public decimal Weight { get; set; }
        public int StockQuantity { get; set; }
        public int StockId { get; set; }
    }
}
