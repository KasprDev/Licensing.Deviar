using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Deviar.Data
{
    public class ResellerSoftware
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }

        public virtual Reseller Reseller { get; set; }
        public int ResellerId { get; set; }
    }
}
