namespace MilitaryTask.Model
{
    public class Tender
    {
        public int Id { get; set; }
        public string TenderId { get; set; }
        public string Name { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
