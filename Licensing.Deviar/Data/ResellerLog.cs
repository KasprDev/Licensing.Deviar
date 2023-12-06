using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Deviar.Data
{
    public class ResellerLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public virtual Reseller Reseller { get; set; }
        public int LicenseKeyId { get; set; }
        public virtual LicenseKey LicenseKey { get; set; }
    }
}
