namespace MilitaryASPWeb.BussinessLogic.Model
{
    public class ProductMappingException : Exception
    {
        public string Message {  get; set; }

        public ProductMappingException() { }

        public ProductMappingException(string message)
        {
            Message = message;
        }

        public ProductMappingException(string message, Exception inner)
           : base(message, inner)
        {
            Message = message;
        }
    }
}
