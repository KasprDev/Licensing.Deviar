namespace Licensing.Deviar.Models
{
    public class ValidateLicenseDto
    {
        public string Key { get; set; }
        public string Fingerprint { get; set; }
        public int SoftwareId { get; set; }
    }
}
