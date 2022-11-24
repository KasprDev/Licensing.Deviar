namespace Licensing.Deviar.Models
{
    public class UsageLogDto
    {
        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime ActionTime { get; set; }
        public string Note { get; set; }
    }
}
