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

        public async Task<List<DadosProxy>> ExecuteWithLimit(List<string> urls, ExtractorService extractor)
        {
            var proxies = new List<DadosProxy>();
            var tasks = new List<Task>();

            foreach (var url in urls) {
                var task = Task.Run(async () =>
                {
                    try
                    {
                        var dados = await extractor.ExtractProxy(url);
                        lock (proxies)
                        {
                            proxies.AddRange(dados);
                        }
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
