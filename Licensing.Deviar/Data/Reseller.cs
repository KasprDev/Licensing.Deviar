using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licensing.Deviar.Data
{
    public class Reseller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
        public int SoftwareId { get; set; }
        public DateTime Added { get; set; }
        public virtual Software Software { get; set; }
        public virtual ICollection<ResellerLog> Logs { get; set; }
    }
}
