using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebCrawler.Data
{
    public class WebCrawlerContextFactory : IDesignTimeDbContextFactory<WebCrawlerContext>
    {
        public WebCrawlerContext CreateDbContext(string[] args) 
        {
            Env.Load(Path.Combine("C:/Projetos/WebCrawler/WebCrawler/", ".env"));
            var connectionString = Environment.GetEnvironmentVariable("LocalConnectString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("A string de conexão não foi encontrada no arquivo .env.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<WebCrawlerContext>();
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 31)));

            return new WebCrawlerContext(optionsBuilder.Options);
        }
    }
}
