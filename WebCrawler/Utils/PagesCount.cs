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

                var pageLinks = doc.DocumentNode.SelectNodes("//a[contains(@href, '/page/') or contains(@class, 'next')]");

                if (pageLinks != null && pageLinks.Count > 0)
                {
                    int maxPageNumber = 0;
                    foreach (var link in pageLinks)
                    {
                        string href = link.GetAttributeValue("href", string.Empty);

                        if (!string.IsNullOrEmpty(href) && href.Contains("/page/"))
                        {
                            string page = href.Split("/page/").Last();
                            Console.WriteLine(page);
                            if (int.TryParse(page, out int pageNumber))
                            {
                                if (pageNumber > maxPageNumber)
                                {
                                    maxPageNumber = pageNumber;
                                }
                            }
                        }
                    }

                    Console.WriteLine("Maior pagina: " + maxPageNumber);

                    for (int i = 2; i <= maxPageNumber; i++)
                    {
                        string urlFull = MainUrl + $"/page/{i}";
                        urls.Add(urlFull);
                        Console.WriteLine("Pagina: " + i + " " + urlFull);
                    }
                }
            }

            Console.WriteLine("Total de páginas encontradas: " + urls.Count);

            return urls;
        }
    }
}
