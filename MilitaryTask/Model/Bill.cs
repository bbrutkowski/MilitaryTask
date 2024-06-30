namespace MilitaryTask.Model
{
    public class Bill
    {
        public int Id { get; set; } // key
        public string BillId { get; set; } // Id from JSON
        public DateTime OccurredAt { get; set; }

        public int BillTypeId { get; set; }
        public BillType BillType { get; set; }

        public int AmountId { get; set; }
        public Amount Amount { get; set; }

        public int TaxRateId { get; set; }
        public TaxRate TaxRate { get; set; }

        public int AccountBalanceId { get; set; }
        public AccountBalance AccountBalance { get; set; }

        public int TenderId { get; set; }
        public Tender Tender { get; set; }
    }
}
