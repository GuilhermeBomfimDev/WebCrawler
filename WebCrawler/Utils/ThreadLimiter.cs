using WebCrawler.Models;
using WebCrawler.Services;

namespace WebCrawler.Utils
{
    public class ThreadLimiter
    {
        private readonly int _maxThreads = 3;
        private readonly SemaphoreSlim _semaphore;

        public ThreadLimiter()
        {
            _semaphore = new SemaphoreSlim(_maxThreads);
        }

        public async Task<List<DadosProxy>> ExecuteWithLimit(List<string> filePaths, ExtractorService extractor)
        {
            var proxies = new List<DadosProxy>();
            var tasks = new List<Task>();

            foreach (var filePath in filePaths) {


                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Arquivo não encontrado: {filePath}");
                    return proxies;
                }

                await _semaphore.WaitAsync();

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var dados = await extractor.ExtractProxy(filePath);
                        lock (proxies)
                        {
                            proxies.AddRange(dados);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao processar {filePath}: {ex.Message}");
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            return proxies;
        }
    }
}
