namespace MilitaryTask.Model
{
    public class Tender
    {
        public int Id { get; set; } // key
        public string TenderId { get; set; } // Id from JSON
        public string Name { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
