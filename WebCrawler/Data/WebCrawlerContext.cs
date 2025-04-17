using Microsoft.EntityFrameworkCore;
using WebCrawler.Models;

namespace WebCrawler.Data
{
    public class WebCrawlerContext : DbContext
    {
        public WebCrawlerContext(DbContextOptions<WebCrawlerContext> options) : base(options)
        { 
        }

        public DbSet<DadosLog> DadosLog { get; set; }
    }
}
