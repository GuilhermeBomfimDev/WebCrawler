using WebCrawler.Controllers;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Iniciando o WebCrawler...");

        var controller = new WebCrawlerController();
        await controller.StartProject();

        Console.WriteLine("Execução finalizada");
    }
}