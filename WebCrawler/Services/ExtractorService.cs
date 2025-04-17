using HtmlAgilityPack;
using System.Xml;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class ExtractorService
    {
        public async Task<List<DadosProxy>> ExtractProxy(string directoryPath)
        {
            var proxies = new List<DadosProxy>();

            var htmlFiles = Directory.GetFiles(directoryPath, "*.html");

            foreach (var file in htmlFiles)
            {
                var doc = new HtmlDocument();
                doc.Load(file);

                var rows = doc.DocumentNode.SelectNodes("//tbody/tr");

                if (rows == null)
                {
                    continue;
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
            }

            // 1. Baixa o HTML
            // 2. Localiza a tabela
            // 3. Para cada linha da tabela (exceto header):
            //    - Extrai IP, Port, Country, Protocol
            //    - Cria ProxyData e adiciona na lista

            return proxies;
        }
    }
}
