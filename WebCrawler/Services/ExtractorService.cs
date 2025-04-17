using HtmlAgilityPack;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class ExtractorService
    {
        public async Task<List<DadosProxy>> ExtractProxy(string url)
        {
            var proxies = new List<DadosProxy>();

            // 1. Baixa o HTML
            // 2. Localiza a tabela
            // 3. Para cada linha da tabela (exceto header):
            //    - Extrai IP, Port, Country, Protocol
            //    - Cria ProxyData e adiciona na lista

            return proxies;
        }
    }
}
