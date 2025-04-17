using WebCrawler.Services;
using WebCrawler.Utils;

namespace WebCrawler.Controllers
{
    public class WebCrawlerController
    {
        public async Task StartProject()
        {
            // 1. Registrar horário de início
            DateTime startService = DateTime.UtcNow;

            var pagesCount = new PagesCount();
            var urls = await pagesCount.GetAllPages();

            var saveHtml = new HtmlSaverService();
            await saveHtml.SaveAllHtml(urls);

            DateTime endService = DateTime.UtcNow;
            Console.WriteLine(endService);
        }
    }
}
