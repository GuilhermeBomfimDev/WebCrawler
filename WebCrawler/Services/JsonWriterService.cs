using System.Text.Json;
using WebCrawler.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebCrawler.Services
{
    public class JsonWriterService
    {
        public async Task<string> SaveToJsonAsync (List<DadosProxy> dadosProxy, string fileName)
        {
            string directory = "C:/Projetos/WebCrawler/WebCrawler/Jsons";

            if (!Directory.Exists (directory))
            {
                Directory.CreateDirectory (directory);
            }
            string filePath = Path.Combine(directory, fileName);

            var json = JsonSerializer.Serialize(dadosProxy, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync (filePath, json);

            return filePath;
        }
    }
}
