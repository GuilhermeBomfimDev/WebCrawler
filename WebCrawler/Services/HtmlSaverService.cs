using HtmlAgilityPack;

namespace WebCrawler.Services
{
    public class HtmlSaverService
    {
        public async Task<List<string>> SaveAllHtml(List<string> urls)
        {
            var htmlList = new List<string>();
            int indexPagePrint = 1;

            foreach (var url in urls)
            {
                using (HttpClient client = new HttpClient())
                {
                    var html = await client.GetStringAsync(url);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    htmlList.Add(html);

                    string fileName = $"C:/Projetos/WebCrawler/WebCrawler/PrintHtml/pagina-{indexPagePrint}.html";
                    await File.WriteAllTextAsync(fileName, html);
                    indexPagePrint++;
                }
            }

            return htmlList;
        }
    }
}
