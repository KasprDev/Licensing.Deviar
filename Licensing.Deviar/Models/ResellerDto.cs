namespace Licensing.Deviar.Models
{
    public class ResellerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public int SoftwareId { get; set; }
        public DateTime Added { get; set; }
        public decimal Percentage { get; set; }
    }
}
