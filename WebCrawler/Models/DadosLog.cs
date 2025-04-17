namespace WebCrawler.Models
{
    public class DadosLog
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPages { get; set; }
        public int TotalLines { get; set; }
        public string JsonFile { get; set; }

        public DadosLog()
        {
        }
    }
}
