namespace MilitaryASPWeb.Models.Model.Exceptions
{
    public class ProcessXmlFileException : Exception
    {
        public string Message {  get; set; }

        public ProcessXmlFileException() { }

        public ProcessXmlFileException(string message)
        {
            Message = message;
        }

        public ProcessXmlFileException(string message, Exception inner)
           : base(message, inner)
        {
            Message = message;
        }
    }
}
