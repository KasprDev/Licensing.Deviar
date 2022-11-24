namespace Licensing.Deviar.Models
{
    public class LicenseKeyDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string HardwareId { get; set; }
        public DateTime LastUsed { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Locked { get; set; }
        public int SoftwareId { get; set; }
    }
}
