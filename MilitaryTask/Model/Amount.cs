namespace MilitaryTask.Model
{
    public class Amount
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Currency { get; set; }

        public Bill Bill { get; set; }
    }
}
