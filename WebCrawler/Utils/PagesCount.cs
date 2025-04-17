using HtmlAgilityPack;
using System.Diagnostics;

namespace WebCrawler.Utils
{
    public class PagesCount
    {
        private const string MainUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";

        public async Task<List<string>> GetAllPages()
        {
            var urls = new List<string>();

            urls.Add(MainUrl); // Save Page 1

            using (HttpClient client = new HttpClient())
            {
                var html = await client.GetStringAsync(MainUrl);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var pageLinks = doc.DocumentNode
                    .SelectNodes("//ul[contains(@class, 'pagination')]//a");

                if (pageLinks != null)
                {
                    int indexPage = 2;
                    foreach (var link in pageLinks)
                    {
                        string href = link.GetAttributeValue("href", string.Empty);

                        if (!string.IsNullOrEmpty(href) && href.Contains("/page/"))
                        {
                            string page = href.Split("/page/").Last();
                            int pageNumber = int.Parse(page);

                            if (pageNumber > indexPage)
                            {
                                int lastPage = pageNumber;

                                for (int i = 2; i <= lastPage; i++)
                                {
                                    string urlFull = MainUrl + $"/page/{i}";
                                    urls.Add(urlFull);
                                }
                            }
                            indexPage++;
                        }
                    }
                }
            }

            // 1. Baixa HTML da página inicial
            // 2. Descobre o total de páginas
            // 3. Monta as URLs de todas as páginas e retorna

            return urls;
        }
    }
}
