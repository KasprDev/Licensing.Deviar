using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Deviar.Data
{
    public class UsageLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public DateTime ActionTime { get; set; }
        public string Note { get; set; }
    }
}
