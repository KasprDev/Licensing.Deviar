using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licensing.Deviar.Data
{
    public class LicenseKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Key { get; set; }
        public DateTime? ActivatedOn { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? HardwareId { get; set; }
        public DateTime LastUsed { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string? IpAddress { get; set; }
        public string? MacAddress { get; set; }
        public string? DeviceName { get; set; }
        public bool Locked { get; set; }
        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }
    }
}