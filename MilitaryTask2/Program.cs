
using MilitaryTask2;

internal class Program
{
    private static void Main(string[] args)
    {
        new Program().Run();
    }

    private void Run()
    {
        var fileService = new FileService();
        fileService.ProcessXmlFiles();
        Console.ReadLine();
    }
}