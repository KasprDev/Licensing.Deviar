using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licensing.Deviar.Data
{
    public class Software
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SellyProductId { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Version { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<LicenseKey> LicenseKeys { get; set; }
    }
}