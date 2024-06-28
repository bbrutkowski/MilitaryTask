namespace MilitaryTask.Model
{
    public class TaxRate
    {
        public int Id { get; set; }
        public string Percentage { get; set; }

        public Bill Bill { get; set; }
    }
}
