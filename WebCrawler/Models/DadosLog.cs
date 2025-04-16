namespace WebCrawler.Models
{
    class DadosLog
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPages { get; set; }
        public int TotalLines { get; set; }
        public string JsonFile { get; set; }
    }
}
