using System.Text.Json;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class JsonWriterService
    {
        public async Task<string> SaveToJsonAsync (List<DadosProxy> proxies)
        {
            string directory = "C:/Projetos/WebCrawler/WebCrawler/Resultado";

            if (!Directory.Exists (directory))
            {
                Directory.CreateDirectory (directory);
            }

            string filePath = Path.Combine(directory, $"proxies_{DateTime.Now:yyyyMMdd_HHmmss}.json");

            var json = JsonSerializer.Serialize (proxies);
            await File.WriteAllTextAsync (filePath, json);

            return filePath;
        }
    }
}
