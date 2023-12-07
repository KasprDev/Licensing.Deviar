namespace Licensing.Deviar.Models
{
    public class SoftwareDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int AmountSold { get; set; }
        public int Resellers { get; set; }
        public string StripeProductId { get; set; }
        public double Version { get; set; }
        public string UserId { get; set; }
        public int Licenses { get; set; }
        public decimal Price { get; set; }
        public LicenseKeyDto[] LicenseKeys { get; set; }
    }
}