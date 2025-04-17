using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebCrawler.Data;
using WebCrawler.Models;
using WebCrawler.Services;
using WebCrawler.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebCrawler.Controllers
{
    public class WebCrawlerController
    {
        public async Task StartProject()
        {
            DateTime startService = DateTime.UtcNow;
            Console.WriteLine("Hora inicial: " + startService + "\n");

            Console.WriteLine("Obtendo as URLs de cada página\n");
            var pagesCount = new PagesCount();
            var urls = await pagesCount.GetAllPages();

            Console.WriteLine("Salvando os prints HTML de cada URL\n");
            var saveHtml = new HtmlSaverService();
            await saveHtml.SaveAllHtml(urls);

            Console.WriteLine("Obtendo os dados de cada linha da tabela\n");
            var getDataHtml = new ExtractorService();
            var threadLimiter = new ThreadLimiter();
            var filePaths = Directory.GetFiles("C:/Projetos/WebCrawler/WebCrawler/PrintHtml/", "*.html").ToList();
            var dataProxies = await threadLimiter.ExecuteWithLimit(filePaths, getDataHtml);

            Console.WriteLine("Salvando o arquivo Json\n");
            var saveJson = new JsonWriterService();
            var jsonFileName = $"dataProxies_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            await saveJson.SaveToJsonAsync(dataProxies, jsonFileName);

            DateTime endService = DateTime.UtcNow;
            Console.WriteLine("Hora final: " + endService + "\n");

            Console.WriteLine("Salvando no banco de dados\n");
            var factory = new WebCrawlerContextFactory();
            using var context = factory.CreateDbContext(Array.Empty<string>());

            var log = new DadosLog
            {
                StartTime = startService,
                EndTime = endService,
                TotalPages = urls.Count,
                TotalLines = dataProxies.Count,
                JsonFile = jsonFileName
            };

            await context.DadosLog.AddAsync(log);
            await context.SaveChangesAsync();

            Console.WriteLine("Log salvo no banco com sucesso!\n");
        }
    }
}
