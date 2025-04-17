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
            Console.WriteLine("Hora inicial: " + startService);

            Console.WriteLine("Obtendo as URLs de cada página");
            var pagesCount = new PagesCount();
            var urls = await pagesCount.GetAllPages();

            Console.WriteLine("Salvando os prints HTML de cada URL");
            var saveHtml = new HtmlSaverService();
            await saveHtml.SaveAllHtml(urls);

            Console.WriteLine("Obtendo os dados de cada linha da tabela");
            var getDataHtml = new ExtractorService();
            var dataProxies = await getDataHtml.ExtractProxy("C:/Projetos/WebCrawler/WebCrawler/PrintHtml/");

            foreach (var proxy in dataProxies)
            {
                Console.WriteLine($"Ip:{proxy.IpAddress} - Port:{proxy.Port} - Country:{proxy.Country} - Protocol:{proxy.Protocol}");
            }

            DateTime endService = DateTime.UtcNow;
            Console.WriteLine("Hora final: " + endService);
        }
    }
}
