using Microsoft.AspNetCore.Identity;

namespace Licensing.Deviar.Data
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Software> Software { get; set; }

        public int? ResellerId { get; set; }
        public virtual Reseller Reseller { get; set; }
    }
}
