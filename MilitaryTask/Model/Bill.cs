namespace MilitaryTask.Model
{
    public class Bill
    {
        public string Id { get; set; }
        public DateTime OccurredAt { get; set; }

        public string TenderId { get; set; }
        public Tender Tender { get; set; }

        public string BillTypeId { get; set; }
        public BillType BillType { get; set; }

        public int AmountId { get; set; }
        public Amount Amount { get; set; }

        public int TaxRateId { get; set; }
        public TaxRate TaxRate { get; set; }

        public int AccountBalanceId { get; set; }
        public AccountBalance AccountBalance { get; set; }
    }
}
