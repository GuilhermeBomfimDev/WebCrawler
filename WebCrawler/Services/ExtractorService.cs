using HtmlAgilityPack;
using System.Xml;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class ExtractorService
    {
        public async Task<List<DadosProxy>> ExtractProxy(string filePath)
        {
            var proxies = new List<DadosProxy>();

            var doc = new HtmlDocument();
            doc.Load(filePath);

            var rows = doc.DocumentNode.SelectNodes("//tbody/tr");

            if (rows == null)
            {
                return proxies;
            }

            foreach (var row in rows)
            {
                var columns = row.SelectNodes("td");

                if (columns == null || columns.Count < 7)
                {
                    continue;
                }

                string ip = columns[1].InnerText.Trim();
                string port = columns[2].SelectSingleNode(".//span")?.GetAttributeValue("data-port", "");
                string country = columns[3].InnerText.Trim();
                string protocol = columns[6].InnerText.Trim();

                proxies.Add(new DadosProxy
                {
                    IpAddress = ip,
                    Port = port,
                    Country = country,
                    Protocol = protocol
                });
            }

            return proxies;
        }
    }
}
