namespace MilitaryTask.Model
{
    public class AccountBalance
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Currency { get; set; }

        public Bill Bill { get; set; } 
    }
}
