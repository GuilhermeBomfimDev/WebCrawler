using DotNetEnv;
using WebCrawler.Controllers;

public class Program
{
    public static async Task Main(string[] args)
    {
        Env.Load();
        Console.WriteLine("Iniciando o WebCrawler...");

        var controller = new WebCrawlerController();
        await controller.StartProject();

        Console.WriteLine("Execução finalizada");
    }
}