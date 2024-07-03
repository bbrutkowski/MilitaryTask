namespace MilitaryTask.Model.DTO
{
    public class OfferAmountsDto
    {
        public string OfferId { get; set; }
        public List<AmountDto> Amounts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
