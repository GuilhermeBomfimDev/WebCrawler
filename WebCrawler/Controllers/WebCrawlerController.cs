using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Controllers
{
    public class WebCrawlerController
    {
        public async Task StartProject()
        {
            DateTime startService = DateTime.Now;
            Console.WriteLine(startService);

            DateTime endService = DateTime.Now;
            Console.WriteLine(endService);

            Console.WriteLine("WebCrawler iniciado.");

            Console.WriteLine("WebCrawler finalizado.");
        }
    }
}
