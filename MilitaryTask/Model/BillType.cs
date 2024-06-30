namespace MilitaryTask.Model
{
    public class BillType
    {
        public int Id { get; set; } // key
        public string BillTypeId { get; set; } // Id from JSON
        public string Name { get; set; }

        public Bill Bill { get; set; }
    }
}
