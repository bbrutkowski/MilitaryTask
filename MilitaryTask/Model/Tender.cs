namespace MilitaryTask.Model
{
    public class Tender
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
