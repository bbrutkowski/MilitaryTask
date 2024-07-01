namespace MilitaryTask.Model
{
    public class Offer
    {
        public int Id { get; set; } // key
        public string OfferId { get; set; } // Id from JSON
        public string Name { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
