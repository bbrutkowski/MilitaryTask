namespace MilitaryASPWeb.BussinessLogic.Model
{ 
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int Quantity { get; set; }
        public bool IsFavorite { get; set; }
    }
}
