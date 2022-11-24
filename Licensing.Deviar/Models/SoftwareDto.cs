namespace Licensing.Deviar.Models
{
    public class SoftwareDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Version { get; set; }
        public string UserId { get; set; }
        public int Licenses { get; set; }
        public LicenseKeyDto[] LicenseKeys { get; set; }
    }
}
