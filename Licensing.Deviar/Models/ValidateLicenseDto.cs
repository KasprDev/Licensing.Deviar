namespace Licensing.Deviar.Models
{
    public class ValidateLicenseDto
    {
        public string Key { get; set; }
        public string Fingerprint { get; set; }
        public int SoftwareId { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string DeviceName { get; set; }
    }
}
